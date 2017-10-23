using Newtonsoft.Json;

namespace RosreestrRestClient.Types {
    /// <summary>
    /// Описывает элемент массива результатов, найденных в Росреестре по какому-то условию.
    /// </summary>
    public class SearchResult : IRosreestrData {
        public string objectId { get; set; }       // ИД объекта
        public int srcObject { get; set; }         // 1 - ГКН, 2 - ЕГРП
        public int regionKey { get; set; }         // ключ региона
        public string objectType { get; set; }     // 002002002000 для premises (помещения), 002009000000 для parcel (ОКС/ЗУ)
        public string objectCn { get; set; }       // полный кад.номер объекта
        public string objectCon { get; set; }      // полный номер права
        public string subjectId { get; set; }      // код субъекта РФ по ОКАТО
        public string regionId { get; set; }       // код дочернего региона в субъекте РФ по ОКАТО
        public string settlementId { get; set; }   // код населённого пункта по ОКАТО
        public string street { get; set; }         // [геоним|тип геонима], например: ЛЕСНАЯ|УЛ
        public string house { get; set; }          // [дом|корпус|строение], например: "12а|2|" или "4||"
        public string addressNotes { get; set; }   // полный неструктурированный адрес
        public string okato { get; set; }          // код объекта по ОКАТО (часто пустой)
        public string apartment { get; set; }      // номер квартиры
        public string nobjectCn { get; set; }      // идентификатор объекта
        public string nobjectCon { get; set; }     // идентификатор права (для прав)
        /// <summary>
        /// Идентификатор записи. Может быть использован как параметр для метода RestRequester.GetById(string id).
        /// </summary>
        [JsonIgnore] public string ID { get { return this.objectId; } }

        /// <summary>
        /// Номер регионального подразделения. Может быть использован как параметр при поиске по номеру права.
        /// </summary>
        [JsonIgnore] public int RegionKey { get { return this.regionKey; } }

        /// <summary>
        /// Кадастровый номер объекта. Может быть использован как параметр для метода RestRequester.GetByCadNum(string cadNum).
        /// </summary>
        [JsonIgnore] public string CadNum { get { return this.objectCn; } }

        /// <summary>
        /// Полный адрес объекта.
        /// </summary>
        [JsonIgnore] public string Address { get { return this.addressNotes; } }

        /// <summary>
        /// Результат поиска как объект учёта.
        /// </summary>
        [JsonIgnore] public CadastralObject CadObject { get { return this.GetCadastralObject(); } }
    }
}
