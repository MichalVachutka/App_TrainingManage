namespace TrainingManage.Data.Interfaces
{
    /// <summary>
    /// Obecné rozhraní pro základní CRUD operace nad entitou.
    /// </summary>
    /// <typeparam name="TEntity">Typ entity.</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Získá seznam všech entit daného typu.
        /// </summary>
        IList<TEntity> GetAll();

        /// <summary>
        /// Najde entitu podle jejího identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor entity.</param>
        /// <returns>Entita nebo null, pokud neexistuje.</returns>
        TEntity? FindById(int id);

        /// <summary>
        /// Vloží novou entitu do úložiště.
        /// </summary>
        /// <param name="entity">Entita k vložení.</param>
        /// <returns>Vložená entita.</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Aktualizuje existující entitu.
        /// </summary>
        /// <param name="entity">Entita k aktualizaci.</param>
        /// <returns>Aktualizovaná entita.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Odstraní entitu podle identifikátoru.
        /// </summary>
        /// <param name="Id">Identifikátor entity k odstranění.</param>
        void Delete(int Id);

        /// <summary>
        /// Ověří existenci entity s daným identifikátorem.
        /// </summary>
        /// <param name="id">Identifikátor entity.</param>
        /// <returns>True, pokud entita existuje, jinak false.</returns>
        bool ExistWithId(int id);
    }
}
