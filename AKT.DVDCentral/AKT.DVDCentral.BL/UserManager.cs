using System.Text;
using System.Security.Cryptography;
using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;


namespace AKT.DVDCentral.BL
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Cannot log in with these credentials.  Your IP address has been saved.")
        {
        }
        public LoginFailureException(string message) : base(message)
        {
        }
    }
    public static class UserManager
    {
        private static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hash.ComputeHash(hashbytes));
            }
        }
        public static int DeleteAll()
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    dc.tblUsers.RemoveRange(dc.tblUsers.ToList());
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insert(User user, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblUser row = new tblUser();

                    row.ID = dc.tblUsers.Any() ? dc.tblUsers.Max(s => s.ID) + 1 : 1;
                    row.FirstName = user.FirstName;
                    row.LastName = user.LastName;
                    row.Username = user.Username;
                    row.Password = GetHash(user.Password);
                    dc.tblUsers.Add(row);
                    results = dc.SaveChanges();
                    if (rollback) transaction.Rollback();

                    user.ID = row.ID;
                    return results;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool Login(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Username))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using (DVDCentralEntities dc = new DVDCentralEntities())
                        {
                            tblUser tblUser = dc.tblUsers.FirstOrDefault(u => u.Username == user.Username);
                            if (tblUser != null)
                            {
                                if (tblUser.Password == GetHash(user.Password))
                                {
                                    user.FirstName = tblUser.FirstName;
                                    user.LastName = tblUser.LastName;
                                    user.ID = tblUser.ID;
                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException();
                                }
                            }
                            else
                            {
                                throw new LoginFailureException();
                            }
                        }
                    }
                    else
                    {
                        throw new LoginFailureException();
                    }
                }
                else
                {
                    throw new LoginFailureException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void Seed()
        {
            DeleteAll();

            User user = new User
            {
                FirstName = "Abigail",
                LastName = "Thompson",
                Username = "abigail",
                Password = "abc123"
            };
            Insert(user);
            
            user = new User
            {
                FirstName = "Brian",
                LastName = "Foote",
                Username = "bfoote",
                Password = "maple"
            };
            Insert(user);
        }

        public static List<User> Load()
        {
            try
            {
                List<User> rows = new List<User>();

                DVDCentralEntities dc = new DVDCentralEntities();

                dc.tblUsers
                    .ToList()
                    .ForEach(dt => rows.Add(new User
                    {
                        ID = dt.ID,
                        FirstName = dt.FirstName,
                        LastName = dt.LastName,
                        Username = dt.Username,
                        Password = dt.Password
                    }));
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static User LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblUser row = dc.tblUsers.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new User
                        {
                            ID = row.ID,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            Username= row.Username,
                            Password= row.Password
                        };
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

        public static int Update(User user, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUser row = dc.tblUsers.Where(dt => dt.ID == user.ID).FirstOrDefault();

                    row.FirstName = user.FirstName;
                    row.LastName = user.LastName;
                    row.Username = user.Username;
                    row.Password = user.Password;

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