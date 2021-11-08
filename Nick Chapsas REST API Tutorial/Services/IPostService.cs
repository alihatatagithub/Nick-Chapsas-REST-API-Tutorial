using Nick_Chapsas_REST_API_Tutorial.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Services
{
   public interface IPostService
    {
        Task<List<Post>> GetPostAsync();
        Task<bool> CreatePostAsync(Post post);

        Task<Post> GetPostByIdAsync(Guid postId);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(Guid postId);
    }
}
