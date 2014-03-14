using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchRockers.BusinessLayer.Interfaces;
using BenchRockers.Common.Interfaces;
using BenchRockers.DataAccessLayer;
using BenchRockers.Common.DataObjects;

namespace BenchRockers.BusinessLayer.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IDataContext _dataSource;

        //public RecommendationService()
        //{

        //}

        public RecommendationService(IDataContext datasource)
        {
            _dataSource = datasource;
        }
        public List<Common.DataObjects.Recommendation> GetAllRecommendations()
        {
            return _dataSource.Query<Recommendation>().ToList();
        }
    }
}
