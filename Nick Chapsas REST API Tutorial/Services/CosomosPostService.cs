using Cosmonaut;
using Cosmonaut.Extensions;
using Nick_Chapsas_REST_API_Tutorial.Domain;
using Nick_Chapsas_REST_API_Tutorial.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Services
{
    public class CosomosPostService : IPostService
    {

        private readonly ICosmosStore<CosomosPostDto> _cosmosStore;
        public CosomosPostService(ICosmosStore<CosomosPostDto> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

      

        public async Task<List<Post>> GetPostAsync()
        {
            var posts = await _cosmosStore.Query().ToListAsync();
            return posts.Select(x => new Post { Id = Guid.Parse(x.Id), Name = x.Name }).ToList();
        }
        public async Task<bool> CreatePostAsync(Post post)
        {
            var cosomosPost = new CosomosPostDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = post.Name
            };

           var response = await _cosmosStore.AddAsync(cosomosPost);
            post.Id = Guid.Parse(cosomosPost.Id);
            return response.IsSuccess;
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            var post = await _cosmosStore.FindAsync(postId.ToString(), postId.ToString());
            //if (post == null)
            //    return null;
            //return new Post { Id = Guid.Parse(post.Id), Name = post.Name };
            return post == null ? null : new Post { Id = Guid.Parse(post.Id), Name = post.Name };

        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            var cosomosPost = new CosomosPostDto
            {
                Id = post.Id.ToString(),
                Name = post.Name
            };

            var response = await _cosmosStore.UpdateAsync(cosomosPost);
            return response.IsSuccess;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var Response = await _cosmosStore.RemoveByIdAsync(postId.ToString());
            return Response.IsSuccess;
        }
    }
}
