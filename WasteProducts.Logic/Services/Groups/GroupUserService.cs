﻿using AutoMapper;
using System;
using System.Linq;
using WasteProducts.DataAccess.Common.Models.Groups;
using WasteProducts.DataAccess.Common.Repositories.Groups;
using WasteProducts.Logic.Common.Models.Groups;
using WasteProducts.Logic.Common.Services.Groups;

namespace WasteProducts.Logic.Services.Groups
{
    public class GroupUserService : IGroupUserService
    {
        private IGroupRepository _dataBase;
        private readonly IMapper _mapper;

        public GroupUserService(IGroupRepository dataBase, IMapper mapper)
        {
            _dataBase = dataBase;
            _mapper = mapper;
        }

        public void Invite(GroupUser item, string adminId)
        {
            var result = _mapper.Map<GroupUserDB>(item);

            var modelGroupDB = _dataBase.Find<GroupDB>(
                x => x.Id == result.GroupId
                && x.AdminId == adminId
                && x.IsNotDeleted == true).FirstOrDefault();
            if (modelGroupDB == null)
                return;

            var model = _dataBase.Find<GroupUserDB>(
                x => x.UserId == result.UserId
                && x.GroupId == result.GroupId).FirstOrDefault();

            result.IsConfirmed = false;
            result.Modified = DateTime.UtcNow;
            if (model == null)
            {
                _dataBase.Create(result);
            }
            else
            {
                _dataBase.Dispose();
                return;
            }
            _dataBase.Save();
        }

        public async void Kick(GroupUser groupUser, string adminId)
        {
            var group = _dataBase.Find<GroupDB>(
                x => x.Id == groupUser.GroupId
                && x.AdminId == adminId
                && x.IsNotDeleted == true).FirstOrDefault();

            if (group == null) return;

            var groupUserDB = _dataBase.Find<GroupUserDB>(x =>
                x.UserId == groupUser.UserId &&
                x.GroupId == groupUser.GroupId).FirstOrDefault();

            if (groupUser == null) return;

            await _dataBase.DeleteUserFromGroupAsync(groupUser.GroupId, groupUser.UserId);
            _dataBase.Save();
        }

        public void GiveRightToCreateBoards(GroupUser item, string adminId)
        {
            var modelGroupDB = _dataBase.Find<GroupDB>(
                x => x.Id == item.GroupId
                && x.AdminId == adminId).FirstOrDefault();

            if (modelGroupDB == null) return;

            var model = _dataBase.Find<GroupUserDB>(
                x => x.UserId == item.UserId
                && x.GroupId == item.GroupId).FirstOrDefault();

            if (model == null) return;

            model.RightToCreateBoards = true;
            model.Modified = DateTime.UtcNow;

            _dataBase.Update(model);
            _dataBase.Save();
        }

        public void TakeAwayRightToCreateBoards(GroupUser item, string adminId)
        {
            var modelGroupDB = _dataBase.Find<GroupDB>(
                x => x.Id == item.GroupId
                && x.AdminId == adminId).FirstOrDefault();

            if (modelGroupDB == null) return;

            var model = _dataBase.Find<GroupUserDB>(
                x => x.UserId == item.UserId
                && x.GroupId == item.GroupId).FirstOrDefault();

            if (model == null) return;

            model.RightToCreateBoards = false;
            model.Modified = DateTime.UtcNow;

            _dataBase.Update(model);
            _dataBase.Save();
        }
    }
}
