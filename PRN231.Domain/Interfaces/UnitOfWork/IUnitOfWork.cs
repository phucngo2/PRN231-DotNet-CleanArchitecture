﻿using PRN231.Domain.Interfaces.Repositories;

namespace PRN231.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public IAnimeRepository AnimeRepository { get; }
    public IGenreRepository GenreRepository { get; }
    public IUserRepository UserRepository { get; }
}