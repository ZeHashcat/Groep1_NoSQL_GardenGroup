﻿namespace GardenGroupModel
{
    public class UserInstance
    {
        private static UserInstance? instance = null;
        private static readonly object padlock = new object();
        private User user;


        private UserInstance(User user)
        {
            this.user = user;
        }

        public User User { get { return user; } set {user = value; }  }
        

        //Singleton with parameters
        public static UserInstance GetUserInstance(User? user = null)
        {
            try
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null) //EXPLANATION: If first if statement goes off in first instance, at same time in second instance, instance gets created like below,                  
                        {                    //EXPLANATION: then first instance padlocks with intention of creating instance, but now has to do second check preventing another instance from being created.
                            instance = new UserInstance(user);
                        }
                    }
                }
                return instance;
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, user is probably null:\n {ex}");//NOTE: Make this different? <<---`-`/\/\`-`(REMINDER)
            }
        }
        
        public static UserInstance GetInstance()
        {
            return instance;
        }

        public static void Logout()
        {
            if (instance != null)
            {
                lock (padlock)
                {
                    instance = null;
                }
            }
        }
    }
}
