using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Utils;

namespace PersonalAccount.Mappers;

public class GroupMapper : Mapper<GroupEntity, GroupModel>
{
    public override GroupEntity ToEntity(GroupModel model)
    {
        var entity = base.ToEntity(model);
        entity.Name = model.Name;
        entity.Description = model.Description;
        entity.ImageUrl = model.ImageUrl?.ToString();
        return entity;
    }

    public override GroupModel ToModel(GroupEntity entity)
    {
        var model = base.ToModel(entity);
        model.Name = entity.Name;
        model.Description = entity.Description;
        model.ImageUrl = entity.ImageUrl?.ToUri();
        return model;
    }
}