using MyBlog.DataAccess.Abstract;
using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccess.Concrete
{
    public class EfProjectRepository : IProjectRepository
    {
        private CvContext _context;
        public EfProjectRepository(CvContext context)
        {
            _context = context;
        }

        public void AddProject(Project entity)
        {
            _context.Projects.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteProject(int projectId)
        {
            var query = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (query != null)
            {
                _context.Projects.Remove(query);
                _context.SaveChanges();
            }
        }

        public IQueryable<Project> GetAll()
        {
            return _context.Projects;
        }

        public Project GetById(int projectId)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == projectId);
        }

        public void UpdateProject(Project entity)
        {
            var project = GetById(entity.Id);
            if (project!=null)
            {
                project.Description = entity.Description;
                project.ImageUrl = entity.ImageUrl;
            }
            _context.SaveChanges();
        }
    }
}
