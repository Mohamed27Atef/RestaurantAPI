using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurantContext context;
        private readonly UserManager<ApplicationIdentityUser> userManager;


        public UserRepository(RestaurantContext context)
        {
            this.context = context;
            this.userManager = userManager;

        }

        public void Add(User entity)
        {
            context.Users.Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User getUserByApplicationUserId(string applicationUserId)
        {
            return context.Users.Where(u => u.application_user_id == applicationUserId).FirstOrDefault();
        }

        public string getUserImage(int userId)
        {
            return context.Users.Where(r => r.id == userId).Select(u => u.Image).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(User entity)
        {
            context.Users.Update(entity);
        }
        public async Task UpdateProfileAsync(string userName, string userId, string firstName, string lastName, string email, string Location, string phoneNumber)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.UserName = userName;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.User.Location = Location;
                user.PhoneNumber = phoneNumber;

                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                }
            }
        }
    }
}
