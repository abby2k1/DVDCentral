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

        public static List<Movie> LoadByGenreID(int id)
        {
            return Load(id);
        }

            public static List<Movie> Load(int? genreID = null)
        {
            if (genreID == null)
            {
                try
                {
                    List<Movie> rows = new List<Movie>();

                    DVDCentralEntities dc = new DVDCentralEntities();

                    var movies = (from m in dc.tblMovies
                                  join r in dc.tblRatings on m.RatingID equals r.ID
                                  join f in dc.tblFormats on m.FormatID equals f.ID
                                  join d in dc.tblDirectors on m.DirectorID equals d.ID
                                  select new
                                  {
                                      MovieID = m.ID,
                                      MovieTitle = m.Title,
                                      MovieDescription = m.Description,
                                      MovieCost = m.Cost,
                                      RatingID = r.ID,
                                      FormatID = f.ID,
                                      DirectorID = d.ID,
                                      MovieInStkQty = m.InStkQty,
                                      MovieImagePath = m.ImagePath,
                                      RatingDesc = r.Description,
                                      FormatDesc = f.Description,
                                      d.FirstName,
                                      d.LastName
                                  }).ToList();

                    movies.ForEach(m => rows.Add(new Models.Movie
                    {
                        ID = m.MovieID,
                        Title = m.MovieTitle,
                        Description = m.MovieDescription,
                        Cost = m.MovieCost,
                        RatingID = m.RatingID,
                        FormatID = m.FormatID,
                        DirectorID = m.DirectorID,
                        InStkQty = m.MovieInStkQty,
                        ImagePath = m.MovieImagePath,
                        RatingDesc = m.RatingDesc,
                        FormatDesc = m.FormatDesc,
                        DirectorName = m.FirstName + " " + m.LastName
                    }));

                    return rows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                try
                {
                    List<Movie> rows = new List<Movie>();

                    DVDCentralEntities dc = new DVDCentralEntities();

                    var movies = (from mg in dc.tblMovieGenres
                                  join m in dc.tblMovies on mg.MovieID equals m.ID
                                  join r in dc.tblRatings on m.RatingID equals r.ID
                                  join f in dc.tblFormats on m.FormatID equals f.ID
                                  join d in dc.tblDirectors on m.DirectorID equals d.ID
                                  where mg.GenreID == genreID || genreID == null
                                  orderby m.ID
                                  select new
                                  {
                                      MovieID = m.ID,
                                      MovieTitle = m.Title,
                                      MovieDescription = m.Description,
                                      MovieCost = m.Cost,
                                      RatingID = r.ID,
                                      FormatID = f.ID,
                                      DirectorID = d.ID,
                                      MovieInStkQty = m.InStkQty,
                                      MovieImagePath = m.ImagePath,
                                      RatingDesc = r.Description,
                                      FormatDesc = f.Description,
                                      d.FirstName,
                                      d.LastName
                                  }).ToList();

                    movies.ForEach(m => rows.Add(new Models.Movie
                    {
                        ID = m.MovieID,
                        Title = m.MovieTitle,
                        Description = m.MovieDescription,
                        Cost = m.MovieCost,
                        RatingID = m.RatingID,
                        FormatID = m.FormatID,
                        DirectorID = m.DirectorID,
                        InStkQty = m.MovieInStkQty,
                        ImagePath = m.MovieImagePath,
                        RatingDesc = m.RatingDesc,
                        FormatDesc = m.FormatDesc,
                        DirectorName = m.FirstName + " " + m.LastName
                    }));

                    return rows;

                    /*dc.tblMovies
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
                    return rows;*/
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static Movie LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    var row = (from m in dc.tblMovies
                               join r in dc.tblRatings on m.RatingID equals r.ID
                               join f in dc.tblFormats on m.FormatID equals f.ID
                               join d in dc.tblDirectors on m.DirectorID equals d.ID
                               where m.ID == id
                               orderby m.Title
                               select new
                               {
                                   MovieID = m.ID,
                                   MovieTitle = m.Title,
                                   MovieDescription = m.Description,
                                   MovieCost = m.Cost,
                                   RatingID = r.ID,
                                   FormatID = f.ID,
                                   DirectorID = d.ID,
                                   MovieInStkQty = m.InStkQty,
                                   MovieImagePath = m.ImagePath,
                                   RatingDesc = r.Description,
                                   FormatDesc = f.Description,
                                   d.FirstName,
                                   d.LastName
                               }).FirstOrDefault();

                    //tblMovie row = dc.tblMovies.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new Movie //{ ID = row.ID, Title = row.Title, Description = row.Description, Cost = row.Cost, RatingID = row.RatingID, FormatID = row.FormatID, DirectorID = row.DirectorID, InStkQty = row.InStkQty, ImagePath = row.ImagePath };
                        {
                            ID = row.MovieID,
                            Title = row.MovieTitle,
                            Description = row.MovieDescription,
                            Cost = row.MovieCost,
                            RatingID = row.RatingID,
                            FormatID = row.FormatID,
                            DirectorID = row.DirectorID,
                            InStkQty = row.MovieInStkQty,
                            ImagePath = row.MovieImagePath,
                            RatingDesc = row.RatingDesc,
                            FormatDesc = row.FormatDesc,
                            DirectorName = row.FirstName + " " + row.LastName
                        };
                    }
                    else
                    {
                        throw new Exception("Movie was not found.");
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
