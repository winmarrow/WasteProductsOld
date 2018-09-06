﻿using System;
using System.Collections.Generic;

namespace WasteProducts.Logic.Common.Services.Groups
{
    /// <summary>
    /// Board administration service
    /// </summary>
    public interface IGroupBoardService
    {
        /// <summary>
        /// Create new board
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="item">Object</param>
        void Create<T>(T item, string userId) where T : class;

        /// <summary>
        /// Add or corect information on board
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="item">Object</param>
        void Update<T>(T item, string userId) where T : class;

        /// <summary>
        /// Board delete
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="item">Object</param>
        void Delete<T>(T item, string userId) where T : class;

        /// <summary>
        /// Search board by id
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="id">Primary key</param>
        T FindById<T>(Guid id) where T : class;

        /// <summary>
        /// Search board by name
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="id">Board name</param>
        T FindByName<T>(string name) where T : class;
    }
}
