using SQLite;
using System;
using System.Collections.Generic;

namespace iApprove.Repository
{
    /// <summary>
    /// This Class is used for performing database actions as Update, delete.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T>where T : class
    {
        /// <summary>
        /// This object is used for applying lock on database.
        /// </summary>
        public static object locker = new object();

        /// <summary>
        /// This variable is used for creating Sql Connections.
        /// </summary>
        public SQLiteConnection database;

        /// <summary>
        /// Initializes a new instance of the TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        public BaseRepository()
        {
        }

        public abstract IEnumerable<T> Get();

        /// <summary>
        /// This Mehod is used for saving new items to database
        /// </summary>
        /// <param name="item"></param>
        public void Save(T item)
        {
            lock (locker)
            {
                try
                {
                    database.Insert(item);
                }
                catch (Exception ex)
                {
                    var exceptionData = new ExceptionModel
                    {
                        ExceptionOrigin = "BaseRepository:Save",
                        ExceptionMessage = "Exception while inserting data",
                        TimeStamp = DateTime.Now
                    };
                    App.ExceptionHandlerDatabase.Save(exceptionData);                                  
                }               
            }
        }


        /// <summary>
        /// This method is used for deleting all items.
        /// </summary>
        public int Drop()
        {
            lock (locker)
            {
                try
                {
                    return database.DropTable<T>();
                }
                catch (Exception ex)
                {
                    var exceptionData = new ExceptionModel
                    {
                        ExceptionOrigin = "BaseRepository:Delete",
                        ExceptionMessage = "Exception while deleting data",
                        TimeStamp = DateTime.Now
                    };
                    App.ExceptionHandlerDatabase.Save(exceptionData);
                    return 0;
                }
            }
        }


        /// <summary>
        /// This method is used for deleting items based on id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            lock (locker)
            {
                try
                {
                    return database.Delete<T>(id);
                }
                catch (Exception ex)
                {
                    var exceptionData = new ExceptionModel
                    {
                        ExceptionOrigin = "BaseRepository:Delete",
                        ExceptionMessage = "Exception while deleting data",
                        TimeStamp = DateTime.Now
                    };
                    App.ExceptionHandlerDatabase.Save(exceptionData);
                    return 0;
                }                
            }
        }

        /// <summary>
        /// This method is used for updating items in database.
        /// </summary>
        /// <param name="item"></param>
        public void Update(T item)
        {
            lock(locker)
            {
                try
                {
                    database.Update(item);
                }
                catch (Exception Ex)
                {
                    var exceptionData = new ExceptionModel
                    {
                        ExceptionOrigin = "BaseRepository:Update",
                        ExceptionMessage = "Exception while Updating data",
                        TimeStamp = DateTime.Now
                    };
                    App.ExceptionHandlerDatabase.Save(exceptionData);
                }               
            }
        }
    }
}
