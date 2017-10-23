namespace RosreestrRestClient.Types {
    /// <summary>
    /// Описывает макро- (регионы) и дочерние (районы, насел.пункты, садоводства и т.п. до геонимов) адресные объекты.
    /// Например: Санкт-Петербург (макро) > Курортный (дочерний для СПб) > Сестрорецк (дочерний для Курортного).
    /// </summary>
    public class Region {
        /// <summary>
        /// ИД (макро-)региона
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName ="id")]
        public string ID { get; set; }

        /// <summary>
        /// Название (макро-)региона
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
