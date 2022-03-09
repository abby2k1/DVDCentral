using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class DirectorManager
    {
        public static int Insert(Director director, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblDirector row = new tblDirector();

                    row.ID = dc.tblDirectors.Any() ? dc.tblDirectors.Max(dt => dt.ID) + 1 : 1;
                    row.FirstName = director.FirstName;
                    row.LastName = director.LastName;

                    dc.tblDirectors.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    director.ID = row.ID;

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Update(Director director, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblDirector row = dc.tblDirectors.Where(dt => dt.ID == director.ID).FirstOrDefault();

                    row.FirstName = director.FirstName;
                    row.LastName = director.LastName;

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

                    tblDirector row = dc.tblDirectors.Where(dt => dt.ID == id).FirstOrDefault();

                    tblMovie movieRow = dc.tblMovies.Where(dt => dt.DirectorID == id).FirstOrDefault();

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
                        movieRow = dc.tblMovies.Where(dt => dt.DirectorID == id).FirstOrDefault();
                    }
                    dc.tblDirectors.Remove(row);

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

        public static List<Director> Load()
        {
            try
            {
                List<Director> rows = new List<Director>();

                DVDCentralEntities dc = new DVDCentralEntities();

                dc.tblDirectors
                    .ToList()
                    .ForEach(dt => rows.Add(new Director
                    {
                        ID = dt.ID,
                        FirstName = dt.FirstName,
                        LastName = dt.LastName
                    }));
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Director LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblDirector row = dc.tblDirectors.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new Director { ID = row.ID, FirstName = row.FirstName, LastName = row.LastName };
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
