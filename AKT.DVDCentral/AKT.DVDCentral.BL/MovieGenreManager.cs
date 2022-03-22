using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class MovieGenreManager
    {
        public static int Insert(MovieGenre movieGenre, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre row = new tblMovieGenre();

                    row.ID = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(dt => dt.ID) + 1 : 1;
                    row.MovieID = movieGenre.MovieID;
                    row.GenreID = movieGenre.GenreID;

                    dc.tblMovieGenres.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    movieGenre.ID = row.ID;

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Update(MovieGenre movieGenre, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre row = dc.tblMovieGenres.Where(dt => dt.ID == movieGenre.ID).FirstOrDefault();

                    row.MovieID = movieGenre.MovieID;
                    row.GenreID = movieGenre.GenreID;


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

                    tblMovieGenre row = dc.tblMovieGenres.Where(dt => dt.ID == id).FirstOrDefault();

                    dc.tblMovieGenres.Remove(row);

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
    }
}
