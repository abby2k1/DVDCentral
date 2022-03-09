using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class FormatManager
    {
        public static int Insert(Format format, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblFormat row = new tblFormat();

                    row.ID = dc.tblFormats.Any() ? dc.tblFormats.Max(dt => dt.ID) + 1 : 1;
                    row.Description = format.Description;

                    dc.tblFormats.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    format.ID = row.ID;

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Update(Format format, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblFormat row = dc.tblFormats.Where(dt => dt.ID == format.ID).FirstOrDefault();

                    row.Description = format.Description;

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

                    tblFormat row = dc.tblFormats.Where(dt => dt.ID == id).FirstOrDefault();

                    tblMovie movieRow = dc.tblMovies.Where(dt => dt.FormatID == id).FirstOrDefault();

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
                        movieRow = dc.tblMovies.Where(dt => dt.FormatID == id).FirstOrDefault();
                    }
                    dc.tblFormats.Remove(row);

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

        public static List<Format> Load()
        {
            try
            {
                List<Format> rows = new List<Format>();

                DVDCentralEntities dc = new DVDCentralEntities();

                dc.tblFormats
                    .ToList()
                    .ForEach(dt => rows.Add(new Format
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

        public static Format LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblFormat row = dc.tblFormats.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new Format { ID = row.ID, Description = row.Description };
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
