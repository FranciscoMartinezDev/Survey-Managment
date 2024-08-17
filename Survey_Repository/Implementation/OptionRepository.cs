using Microsoft.EntityFrameworkCore;
using Survey_DataEntry;
using Survey_DataEntry.Entities;
using Survey_Repository.Interfaces;

namespace Survey_Repository.Implementation
{
    public class OptionRepository : BaseRepository<QuestionOption>, IOptionRepository
    {
        public OptionRepository(SurveyDbContext context) : base(context) { }

        public async Task<ICollection<QuestionOption>> Options(IEnumerable<int> questionIds)
        {
            var dbOptions = await GetOneOrMoreAsync(x => questionIds.Contains(x.QuestionId));
            return dbOptions.ToList();
        }

        #region methods

        /// <summary>
        ///  Actualiza las optiones de cada pregunta.
        /// </summary>
        /// <param name="options">
        ///  Lista de opciones a actualizar
        /// </param>
        /// <returns>
        ///  El estado de la solicitud.
        /// </returns>
        public async Task UpdateOptions(ICollection<QuestionOption> options)
        {
            var dbOptions = await GetOneOrMoreAsync(x => options.Select(o => o.QuestionId)
                                                                .Contains(x.QuestionId));

            var existingOptions = dbOptions.Where(x => options.Select(o => o.IdOption)
                                                              .Contains(x.IdOption)).ToList();
            var newOptions = dbOptions.Count < options.Count ? options.Where(x => x.IdOption == 0).ToList() : null;
            var optionsDeleted = dbOptions.Where(x => !options.Select(o => o.IdOption)
                                                              .Contains(x.IdOption)).Where(q => q.IdOption != 0).ToList();
            if (existingOptions.Count() > 0)
            {
                foreach (var opt in existingOptions)
                {
                    var option = options.FirstOrDefault(x => x.IdOption == opt.IdOption);
                    opt.Option = option?.Option;
                }
            }
            if (optionsDeleted.Count() > 0)
            {
                await MultiDelete(optionsDeleted.ToList());
            }
            if (newOptions != null)
            {
                await MultiAddAsync(newOptions);
            }
            await SaveChangeAsync();
        }

        /// <summary>
        ///  Elimina las opciones desvinculadas.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task UnLinkOptions(ICollection<QuestionOption> options)
        {
            await MultiDelete(options);
            await SaveChangeAsync();
        }
        #endregion

    }
}
