﻿using Bot.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot.Backend.Models
{
    public class UserRepository : IRepository<User>
    {
        private BotContexts context = new BotContexts();

        public UserRepository(BotContexts context)
        {
            this.context = context;
        }

        public List<string> GetAll()
        {
            var users = context.Users.Select(x =>x.Username).ToList<string>();
            return users;
        }

        public User Get(string name)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == name);

            if(user != null)
            {
                return user;
            }
            else
            {
                NullReferenceException ex = new NullReferenceException();
                throw ex;
            }
        }

        public void UpdateRaiting(string username, int raiting)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username);

            if(user != null)
            {
                user.Raiting = raiting;
                context.SaveChanges();
            }
            else
            {
                NullReferenceException ex = new NullReferenceException();
                throw ex;
            }
        }

        public void AddUser(string username)
        {
            User user = new User();
            user.Username = username;
            user.Raiting = 0;
            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool IsExist(string username)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username);

            if(user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetCurrentRaiting(string username)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username);

            if(user != null)
            {
                return user.Raiting;
            }
            else
            {
                NullReferenceException ex = new NullReferenceException();
                throw ex;
            }
        }

        public int GetPosition(string username)
        {
            var usersByRaiting = context.Users.OrderByDescending(x => x.Raiting).ToArray();

            if(usersByRaiting != null)
            {
                int position = 0;

                for (int i = 0; i < usersByRaiting.Count(); i++)
                {
                    if(usersByRaiting[i].Username == username)
                    {
                        position = i + 1;
                        break;
                    }
                }

                return position;
            }
            else
            {
                NullReferenceException ex = new NullReferenceException();
                throw ex;
            }
        }
    }
}