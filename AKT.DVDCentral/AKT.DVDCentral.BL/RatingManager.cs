using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class RatingManager
    {
        public static int Insert(Rating rating, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblRating row = new tblRating();

                    row.ID = dc.tblRatings.Any() ? dc.tblRatings.Max(dt => dt.ID) + 1 : 1;
                    row.Description = rating.Description;

                    dc.tblRatings.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    rating.ID = row.ID;

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Update(Rating rating, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblRating row = dc.tblRatings.Where(dt => dt.ID == rating.ID).FirstOrDefault();

                    row.Description = rating.Description;

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblRating row = dc.tblRatings.Where(dt => dt.ID == id).FirstOrDefault();

                    tblMovie movieRow = dc.tblMovies.Where(dt => dt.RatingID == id).FirstOrDefault();

                    while (movieRow != null)
                    {
                        tblMovieGenre movieGenreRow = dc.tblMovieGenres.Where(dt => dt.MovieID == movieRow.ID).FirstOrDefault();
                        tblOrderItem orderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == movieRow.ID).FirstOrDefault();
                        while (movieGenreRow != null)
                        {
                            dc.tblMovieGenres.Remove(movieGenreRow);
                            dc.SaveChanges();
                            movieGenreRow = dc.tblMovieGenres.Where(dt => dt.MovieID == movieRow.ID).FirstOrDefault();
                        }
                        while (orderItemRow != null)
                        {
                            dc.tblOrderItems.Remove(orderItemRow);
                            dc.SaveChanges();
                            orderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == movieRow.ID).FirstOrDefault();
                        }
                        dc.tblMovies.Remove(movieRow);
                        dc.SaveChanges();
                        movieRow = dc.tblMovies.Where(dt => dt.RatingID == id).FirstOrDefault();
                    }
                    dc.tblRatings.Remove(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region "Region"
        #endregion

        public static List<Rating> Load()
        {
            try
            {
                List<Rating> rows = new List<Rating>();

                DVDCentralEntities dc = new DVDCentralEntities();

                dc.tblRatings
                    .ToList()
                    .ForEach(dt => rows.Add(new Rating
                    {
                        ID = dt.ID,
                        Description = dt.Description
                    }));
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Rating LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblRating row = dc.tblRatings.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new Rating { ID = row.ID, Description = row.Description };
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
