using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class GenreManager
    {
        public static int Insert(Genre genre, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGenre row = new tblGenre();

                    row.ID = dc.tblGenres.Any() ? dc.tblGenres.Max(dt => dt.ID) + 1 : 1;
                    row.Description = genre.Description;

                    dc.tblGenres.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    genre.ID = row.ID;

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Update(Genre genre, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGenre row = dc.tblGenres.Where(dt => dt.ID == genre.ID).FirstOrDefault();

                    row.Description = genre.Description;

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

                    tblGenre row = dc.tblGenres.Where(dt => dt.ID == id).FirstOrDefault();

                    tblMovieGenre movieGenreRow = dc.tblMovieGenres.Where(dt => dt.GenreID == id).FirstOrDefault();
                    while (movieGenreRow != null)
                    {
                        dc.tblMovieGenres.Remove(movieGenreRow);
                        dc.SaveChanges();
                        movieGenreRow = dc.tblMovieGenres.Where(dt => dt.MovieID == id).FirstOrDefault();
                    }

                    dc.tblGenres.Remove(row);

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

        public static List<Genre> Load()
        {
            try
            {
                List<Genre> rows = new List<Genre>();

                DVDCentralEntities dc = new DVDCentralEntities();

                dc.tblGenres
                    .ToList()
                    .ForEach(dt => rows.Add(new Genre
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

        public static Genre LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblGenre row = dc.tblGenres.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new Genre { ID = row.ID, Description = row.Description };
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
