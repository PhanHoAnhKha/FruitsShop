using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebFruit.Data;
using WebFruit.DTOs;
using WebFruit.Interfaces;
using WebFruit.Models;

namespace WebFruit.Services
{
    public class NewRepository : INewRepository
    {
        private readonly FruitDbContext _context;

        public NewRepository(FruitDbContext context)
        {
            _context = context;
        }

        public async Task<List<New>> GetNewsAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<New> GetNewsByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task<bool> AddNewsAsync(NewDTO newsDto)
        {
            try
            {
                var news = new New
                {
                    Title = newsDto.Title,
                    ImageUrl = newsDto.ImageUrl,
                    Content = newsDto.Content,
                    ContentDetail = newsDto.ContentDetail,
                    Author = newsDto.Author,
                    CreatedDate = newsDto.CreatedDate,
                    IsHot = newsDto.IsHot,
                };

                _context.News.Add(news);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return false;
            }
        }

        public async Task<bool> UpdateNewsAsync(int id, NewDTO newsDto)
        {
            try
            {
                var news = await _context.News.FindAsync(id);
                if (news == null)
                {
                    return false;
                }

                news.Title = newsDto.Title;
                news.ImageUrl = newsDto.ImageUrl;
                news.Content = newsDto.Content;
                news.ContentDetail = newsDto.ContentDetail;
                news.Author = newsDto.Author;
                news.CreatedDate = newsDto.CreatedDate;
                news.IsHot = newsDto.IsHot;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return false;
            }
        }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            try
            {
                var news = await _context.News.FindAsync(id);
                if (news == null)
                {
                    return false;
                }

                _context.News.Remove(news);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return false;
            }
        }
    }
}
