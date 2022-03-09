using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class MovieManager
    {
        public static int Insert(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie row = new tblMovie();

                    row.ID = dc.tblMovies.Any() ? dc.tblMovies.Max(dt => dt.ID) + 1 : 1;
                    row.Title = movie.Title;
                    row.Description = movie.Description;
                    row.Cost = movie.Cost;
                    row.RatingID = movie.RatingID;
                    row.FormatID = movie.FormatID;
                    row.DirectorID = movie.DirectorID;
                    row.InStkQty = movie.InStkQty;
                    row.ImagePath = movie.ImagePath;

                    dc.tblMovies.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    movie.ID = row.ID;

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Update(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie row = dc.tblMovies.Where(dt => dt.ID == movie.ID).FirstOrDefault();

                    row.Title = movie.Title;
                    row.Description = movie.Description;
                    row.Cost = movie.Cost;
                    row.RatingID = movie.RatingID;
                    row.FormatID = movie.FormatID;
                    row.DirectorID = movie.DirectorID;
                    row.InStkQty = movie.InStkQty;
                    row.ImagePath = movie.ImagePath;

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

                    tblMovie row = dc.tblMovies.Where(dt => dt.ID == id).FirstOrDefault();

                    tblMovieGenre movieGenreRow = dc.tblMovieGenres.Where(dt => dt.MovieID == id).FirstOrDefault();
                    tblOrderItem orderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == id).FirstOrDefault();
                    while (movieGenreRow != null)
                    {
                        dc.tblMovieGenres.Remove(movieGenreRow);
                        dc.SaveChanges();
                        movieGenreRow = dc.tblMovieGenres.Where(dt => dt.MovieID == id).FirstOrDefault();
                    }
                    while (orderItemRow != null)
                    {
                        dc.tblOrderItems.Remove(orderItemRow);
                        dc.SaveChanges();
                        orderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == id).FirstOrDefault();
                    }
                    
                    dc.tblMovies.Remove(row);

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

        public static List<Movie> Load()
        {
            try
            {
                List<Movie> rows = new List<Movie>();

                DVDCentralEntities dc = new DVDCentralEntities();

                dc.tblMovies
                    .ToList()
                    .ForEach(dt => rows.Add(new Movie
                    {
                        ID = dt.ID,
                        Title = dt.Title,
                        Description = dt.Description,
                        Cost = dt.Cost,
                        RatingID = dt.RatingID,
                        FormatID = dt.FormatID,
                        DirectorID = dt.DirectorID,
                        InStkQty = dt.InStkQty,
                        ImagePath = dt.ImagePath
                    }));
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Movie LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovie row = dc.tblMovies.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new Movie { ID = row.ID, Title = row.Title, Description = row.Description, Cost = row.Cost, RatingID = row.RatingID, FormatID = row.FormatID, DirectorID = row.DirectorID, InStkQty = row.InStkQty, ImagePath = row.ImagePath };
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
