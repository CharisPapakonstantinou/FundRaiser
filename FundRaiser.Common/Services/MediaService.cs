using FundRaiser.Common.Database;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaiser.Common.Services
{
    public class MediaService: IMediaService
    {
        private readonly AppDbContext _context;

        public MediaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Media> Create(Media media)
        {
            await _context.Media.AddAsync(media);
            await _context.SaveChangesAsync();

            return media;
        }
        
        public async Task<List<Media>> Create(List<Media> media)
        {
            await _context.Media.AddRangeAsync(media);
            await _context.SaveChangesAsync();

            return media;
        }

        public async Task<bool> Delete(int mediaId)
        {
            var media = await _context.Media.FirstOrDefaultAsync(m => m.Id == mediaId);

            if (media == null)
            {
                return false;
            }

            _context.Media.Remove(media);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Media>> GetMedia(int mediaId)
        {
            return await _context.Media
            .Where(m => m.Id == mediaId)
            .ToListAsync();
        }

        public async Task<Media> Update(int mediaId, Media media)
        {
            var mediaFromDb = await _context.Media.FirstOrDefaultAsync(m => m.Id == mediaId);

            mediaFromDb.Description = media.Description ?? mediaFromDb.Description;
            mediaFromDb.Path = media.Path ?? mediaFromDb.Path;
            mediaFromDb.MediaType = media.MediaType;

            await _context.SaveChangesAsync();

            return mediaFromDb;
        }
        
        public async Task<Media> GetMediaById(int id)
        {
            return await _context.Media.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
