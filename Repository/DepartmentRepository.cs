﻿using Microsoft.EntityFrameworkCore;
using CareProviderPortal.Models;
using CareProviderPortal.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CareProviderPortalContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(CareProviderPortalContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

    public async Task<T> GetById(int id) => await _dbSet.FindAsync(id);

    public async Task<T> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        await _context.Entry(entity).ReloadAsync();
        return entity;
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await GetById(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
