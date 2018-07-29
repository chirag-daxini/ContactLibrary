using ContactLibrary.Data.DataContext;
using ContactLibrary.Data.Repository;
using ContactLibrary.Data.Service;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactLibrary.Web.DependencyResolver
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            _kernel.Bind<IDbContext>().To<SqlDbContext>().InRequestScope();
            _kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            _kernel.Bind<IContactService>().To<ContactLibrary.Data.Service.ContactService>();
        }
    }
}