using GameStore.BLL.Infrastrcture.Models;
using GameStore.BLL.Infrastrcture;
using GameStore.DAL.Domain;
using AutoMapper;
using GameStore.DAL.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using GameStore.DAL.Entities.Games;
using GameStore.BLL.DTO.Games;
using GameStore.DAL.Entities.History;

namespace GameStore.BLL.Services.GamesServices
{
    public class GameReviewService : IGameReviewService
    {
        private readonly GsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GameReviewService(GsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResultServiceModel> CreateReviewGameAsync(GameReviewModel model)
        {
            ResultServiceModel result = new();

            AppUser user = await _dbContext.AppUsers.Where(x => x.Email == model.UserEmail).FirstOrDefaultAsync();
            if (user is null) { result.IsSucceeded = false; return result; }

            GameReview gameReview = new()
            {
                UserId = user.Id,
                GameId = model.GameId,
                Review = model.Review,
                DateCreate = DateTime.UtcNow,
                isPositive = model.isPositive
            };

            try
            {
                await _dbContext.GameReviews.AddAsync(gameReview);
                await _dbContext.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; return result; }


            result.IsSucceeded = true;
            return result;
        }

        public async Task<List<GameReviewDTO>> GetGameReviewsAsync(long gameId, int pageNumber, int pageSize)
        {
            List<GameReview> reviews = await _dbContext.GameReviews.Where(x => x.GameId == gameId && x.isDelete == false).OrderByDescending(x => x.DateCreate)
                                                                   .Include(u => u.User)
                                                                   .Skip((pageNumber - 1) * pageSize)
                                                                   .Take(pageSize).ToListAsync();

            return _mapper.Map<List<GameReviewDTO>>(reviews);
        }

        public async Task<ResultServiceModel> ElasticRemoveReviewAsync(long reviewId)
        {
            ResultServiceModel result = new();

            GameReview review = await _dbContext.GameReviews.Where(x => x.Id == reviewId).FirstOrDefaultAsync();
            if (review is null) { result.IsSucceeded = false; return result; }

            review.isDelete = true;

            try
            {
                _dbContext.GameReviews.Update(review);
                await _dbContext.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; return result; }

            result.IsSucceeded = true;
            return result;
        }
        public async Task<GameReviewDTO> GetGameReviewByIdAsync(long reviewId)
        {
            GameReview review = await _dbContext.GameReviews.Where(x => x.Id == reviewId).FirstOrDefaultAsync();
            return _mapper.Map<GameReviewDTO>(review);
        }

        public async Task<ResultServiceModel> UpdateGameReviewAsync(GameReviewDTO gameReview)
        {
            ResultServiceModel result = new();

            GameReview review = await _dbContext.GameReviews.Where(x => x.Id == gameReview.Id).FirstOrDefaultAsync();
            if (review is null) { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.recordNoExist; return result; }

            //История
            HistoryGameReviews history = new();
            history.ReviewId = review.Id;
            history.OldValue = review.Review;
            history.NewValue = gameReview.Review;
            history.Date = DateTime.UtcNow;

            try
            {
                review.Review = gameReview.Review;
                review.isPositive = gameReview.isPositive;
                review.DateModified = DateTime.UtcNow;

                _dbContext.GameReviews.Update(review);

                await _dbContext.HistoryGameReviews.AddAsync(history);
                await _dbContext.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }



            result.IsSucceeded = true;
            return result;
        }

       public async Task<GameReviewStatsModel> GetGameReviewStatsAsync(long gameId)
       {
            GameReviewStatsModel model = new();

            List<GameReview> gameReviews = await _dbContext.GameReviews.Where(x=>x.GameId == gameId && x.isDelete == false).ToListAsync();

            int totalReview = gameReviews.Count;
            if (totalReview == 0)
            {
                model.Positive = 0;
                model.Negative = 0;
                return model;
            }

            int positive = gameReviews.Count(x => x.isPositive);
            int negative = gameReviews.Count(x => !x.isPositive);

            model.Positive = (positive / (double)totalReview) * 100;
            model.Positive = Math.Round(model.Positive, 1);
            model.Negative = (negative / (double)totalReview) * 100;
            model.Negative = Math.Round(model.Negative, 1);


            return model;
       }
    }
}
