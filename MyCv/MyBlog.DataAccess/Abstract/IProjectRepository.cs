using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccess.Abstract
{
    public interface IProjectRepository
    {
        Project GetById(int projectId);
        IQueryable<Project> GetAll();
        void AddProject(Project entity);
        void UpdateProject(Project entity);
        void DeleteProject(int projectId);
    }
}
