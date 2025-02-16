﻿using EFCore.Toolkit.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Toolkit
{
    public interface IDbContext : IContext
    {
        /// <summary>
        /// The name of this EntityFramework context.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The generic DbSet of type <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}