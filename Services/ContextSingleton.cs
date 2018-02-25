using System;
using System.Linq;
using Dao;
using Entities;
using Services.EntitiesViewModels;

namespace Services
{
    public sealed class ContextSingleton
    {
        private static readonly Lazy<ContextSingleton> lazy =
            new Lazy<ContextSingleton>(() => new ContextSingleton());


        private DaoRegistry _daoRegistry;
        private EntitiesVMRegistry _entitiesVmRegistry;
        private LiteDbModel _model;


        private ContextSingleton()
        {
        }

        public static ContextSingleton Instance => lazy.Value;
        private LiteDbModel Model => _model ?? (_model = LiteDbModel.CreateModel());
        private DaoRegistry DaoRegistry => _daoRegistry ?? (_daoRegistry = new DaoRegistry(Model));

        public EntitiesVMRegistry EntitiesVmRegistry =>
            _entitiesVmRegistry ?? (_entitiesVmRegistry = new EntitiesVMRegistry(DaoRegistry));

        public float SubgroupSize => Instance.EntitiesVmRegistry.Settings[(int) Settings.SubgroupSize]
            .IntValue;

        public float ZachMultiplayer => Instance.EntitiesVmRegistry.Settings[(int) Settings.ZachMultiplayer]
            .FloatValue;

        public float ConsMultiplayer => Instance.EntitiesVmRegistry.Settings[(int) Settings.ConsMultiplayer]
            .FloatValue;

        public float ExamMultiplayer => Instance.EntitiesVmRegistry.Settings[(int) Settings.ExamMultiplayer]
            .FloatValue;

        public float CourseWorkMultiplayer => Instance.EntitiesVmRegistry.Settings[(int) Settings.CourseWorkMultiplayer]
            .FloatValue;

        public float CourseProjectMultiplayer => Instance.EntitiesVmRegistry
            .Settings[(int) Settings.CourseProjectMultiplayer]
            .FloatValue;

        public string GetSpecialty(string stream)
        {
            return Instance.EntitiesVmRegistry.Specialties.SingleOrDefault(set => set.Name == stream.Substring(0, 3))
                .Value;
        }
    }
}