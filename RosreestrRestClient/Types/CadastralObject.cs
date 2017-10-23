using Newtonsoft.Json;

namespace RosreestrRestClient.Types {
    /// <summary>
    /// Описывает объект учёта
    /// </summary>
    public class CadastralObject : IRosreestrData {
        public string objectId { get; set; }            // ИД объекта
        public string type { get; set; }                // видел варианты: premises = помещение, parcel = ЗУ/ОКС
        public int regionKey { get; set; }              // ключ региона
        public int source { get; set; }                 // видел варианты: 1 (для parcel) и 2 (для premises и для parcel). Возможно, 1 - ГКН, 2 - ЕГРП.
        public string firActualDate { get; set; }       // дата актуализации в ФГИС ЕГРН
        public ObjectData objectData { get; set; }      // краткие данные объекта
        public ParcelData parcelData { get; set; }      // подробные данные для parcel
        public RealtyData realtyData { get; set; }      // данные об объекте недвижимости, полученные из ЕГРП; очень редко встречал
        public PremisesData premisesData { get; set; }  // подробные данные для premises
        public RightEncumbranceObject[] rightEncumbranceObjects { get; set; } // сведения о правах
        public OldNumber[] oldNumbers { get; set; }     // предыдущие номера

        /// <summary>
        /// Идентификатор записи. Может быть использован как параметр для метода RestRequester.GetById(string id).
        /// </summary>
        [JsonIgnore] public string ID { get { return this.objectId; } }

        /// <summary>
        /// Номер регионального подразделения. Может быть использован как параметр при поиске по номеру права.
        /// </summary>
        [JsonIgnore] public int RegionKey { get { return this.regionKey; } }

        /// <summary>
        /// Дата актуализации на Портале. Формат: гггг-мм-дд
        /// </summary>
        [JsonIgnore] public string ActualDate { get { return this.firActualDate; } }
        //[JsonIgnore] public DateTime ActualDate { get { return DateTime.ParseExact(this.firActualDate, "yyyy-MM-dd", null); } }

        /// <summary>
        /// Общая информация об объекте: номер, адрес, название и т.п.
        /// </summary>
        [JsonIgnore] public ObjectData GeneralData { get { return this.objectData; } }

        /// <summary>
        /// Является ли данный объект "внешним": ЗУ, зданием, сооружением.
        /// </summary>
        [JsonIgnore] public bool IsOutdoorObject { get { return (this.type == "parcel"); } }

        /// <summary>
        /// Информация об объекте как о "внешнем": ЗУ, здание, сооружение.
        /// </summary>
        [JsonIgnore] public ParcelData AsOutdoorObject { get { return this.parcelData; } }

        /// <summary>
        /// Является ли данный объект "внутренним": помещением.
        /// </summary>
        [JsonIgnore] public bool IsIndoorObject { get { return (this.type == "premises"); } }

        /// <summary>
        /// Информация об объекте как о "внутреннем": помещение.
        /// </summary>
        [JsonIgnore] public PremisesData AsIndoorObject { get { return this.premisesData; } }
    }

    /// <summary>
    /// Описывает общие данные объекта
    /// </summary>
    public class ObjectData : IRosreestrData {
        public string id { get; set; }
        public int regionKey { get; set; }
        public int srcObject { get; set; }
        public string objectType { get; set; }
        public string objectName { get; set; }          // название объекта, например: "Жилой дом"
        public int removed { get; set; }
        public string dateLoad { get; set; }
        public string addressNote { get; set; }         // полный неструктурированный адрес
        public string objectCn { get; set; }            // полный кад.номер
        public string objectCon { get; set; }
        public string objectInv { get; set; }
        public string objectUn { get; set; }
        public string rsCode { get; set; }
        public string actualDate { get; set; }          // дата актуализации в ЕГРН
        public int brkStatus { get; set; }
        public string brkDate { get; set; }
        public string formRights { get; set; }
        public ObjectAddress objectAddress { get; set; }

        /// <summary>
        /// Наименование объекта.
        /// </summary>
        [JsonIgnore] public string Name { get { return this.objectName; } }

        /// <summary>
        /// Кадастровый номер.
        /// </summary>
        [JsonIgnore] public string CadastralNumber { get { return this.objectCn; } }

        /// <summary>
        /// Дата актуализации в Росреестре. Формат: "гггг-мм-дд".
        /// </summary>
        [JsonIgnore] public string UpdateDate { get { return this.actualDate; } }

        /// <summary>
        /// Структурированная информация об адресе.
        /// </summary>
        [JsonIgnore] public ObjectAddress AddressData { get { return this.objectAddress; } }

        /// <summary>
        /// Неструктурированная информация об адресе.
        /// </summary>
        [JsonIgnore] public string AddressString { get { return this.addressNote; } }
    }

    /// <summary>
    /// Описывает подробные адресные данные объекта
    /// </summary>
    public class ObjectAddress : IRosreestrData {
        public string id { get; set; }
        public int regionKey { get; set; }
        public string okato { get; set; }
        public string kladr { get; set; }
        public string region { get; set; }
        public string district { get; set; }
        public string districtType { get; set; }
        public string place { get; set; }
        public string placeType { get; set; }
        public string locality { get; set; }
        public string localityType { get; set; }
        public string street { get; set; }              // название геонима
        public string streetType { get; set; }          // сокр.название типа геонима
        public string house { get; set; }               // номер дома
        public string building { get; set; }            // номер корпуса
        public string structure { get; set; }           // литера? строение?
        public string apartment { get; set; }           // номер квартиры
        public string addressNotes { get; set; }        // полный неструктурированный адрес
        public string mergedAddress { get; set; }       // адрес внутри нас.пункта (геоним и дальше)
    }

    /// <summary>
    /// Описывает подробные данные "внешнего" объекта (ЗУ, здание, сооружение)
    /// </summary>
    public class ParcelData : IRosreestrData {
        public string id { get; set; }
        public int regionKey { get; set; }
        public string parcelCn { get; set; }            // полный кад.номер объекта
        public string parcelStatus { get; set; }        // код статуса объекта: "01" = "Ранее учтенный"
        public string dateCreate { get; set; }          // дата постановки на учет
        public object dateRemove { get; set; }          // дата снятия с учета
        public object categoryType { get; set; }        // код категории земель для ЗУ
        public decimal areaValue { get; set; }          // площадь объекта
        public string areaType { get; set; }
        public string areaUnit { get; set; }
        public object areaTypeValue { get; set; }
        public object areaUnitValue { get; set; }
        public string categoryTypeValue { get; set; }   // название категории земель для ЗУ
        public bool rightsReg { get; set; }             // признак зарегистрированности прав
        public decimal cadCost { get; set; }            // кадастровая стоимость
        public string cadUnit { get; set; }             // код ЕИ кад.стоимости по ОКЕИ, 383 = рубль
        public string dateCost { get; set; }            // дата утверждения кад.стоимости
        public int oksFlag { get; set; }                // признак ОКСа: 0 - неОКС, 1 - ОКС
        public string oksType { get; set; }             // тип ОКС: "building" = здание
        public string oksFloors { get; set; }           // этажность ОКС
        public string oksUFloors { get; set; }          // название подземного этажа ОКС, например: "подвал цокольный"
        public string oksElementsConstruct { get; set; }// код материала конструктивных элементов, если несколько - через ", "
        public string oksYearUsed { get; set; }         // год ввода ОКС в эксплуатацию
        public decimal oksInventoryCost { get; set; }   // инвентарная стоимость
        public string oksInn { get; set; }
        public string oksExecutor { get; set; }
        public string oksYearBuilt { get; set; }
        public string oksCostDate { get; set; }
        public object rcType { get; set; }
        public string rcDate { get; set; }              // признак расшифрованности сведений о КИ. Если "ci" - см.нижеследующие  поля
        public string guidUl { get; set; }
        public string guidFl { get; set; }
        public string ciSurname { get; set; }           // фамилия КИ
        public string ciFirst { get; set; }             // имя КИ
        public string ciPatronymic { get; set; }        // отчество КИ
        public string ciNCertificate { get; set; }      // № сертификата КИ
        public string ciPhone { get; set; }             // телефон КИ
        public string ciEmail { get; set; }             // эл.почта КИ
        public string ciAddress { get; set; }           // адрес КИ
        public string coName { get; set; }              // название организации КИ
        public string coInn { get; set; }               // ИНН организации КИ
        public string utilCode { get; set; }            // код разрешенного использования по классификатору
        public string utilByDoc { get; set; }           // название разрешенного использования по документам
        public object cadastralBlockId { get; set; }    // ИД кадастрового квартала (часто пустой)
        public string parcelStatusStr { get; set; }     // название статуса объекта
        public string oksElementsConstructStr { get; set; }// название материала конструктивных элемементов, если несколько - через ", " и каждый с заглавной
        public object utilCodeDesc { get; set; }
    }


    public class RealtyData : IRosreestrData {
        public string id { get; set; }
        public int regionKey { get; set; }
        public string realtyCn { get; set; }
        public string realtyCon { get; set; }
        public string realtyInv { get; set; }
        public string realtyUn { get; set; }
        public string literBti { get; set; }
        public string realtyType { get; set; }
        public string assignType { get; set; }
        public string realtyName { get; set; }
        public decimal areaValue { get; set; }
        public string areaType { get; set; }
        public string areaUnit { get; set; }
        public string floorGround { get; set; }
        public string floorUnder { get; set; }
        public string floorGroundStr { get; set; }
        public string floorUnderStr { get; set; }
        public string realtyTypeValue { get; set; }
        public string areaUnitValue { get; set; }
        public bool rightsReg { get; set; }
        public bool multiFlat { get; set; }
        public bool incomplete { get; set; }
        public string realtyTypeStr { get; set; }
    }

    /// <summary>
    /// Описывает подробные данные "внутреннего" объекта (помещение)
    /// </summary>
    public class PremisesData : IRosreestrData {
        public string id { get; set; }                  // ИД объекта
        public int regionKey { get; set; }
        public string premisesCn { get; set; }          // полный кад.номер объекта
        public string premisesCon { get; set; }         // полный номер права на объект, например: "02-04-17/043/2005-434"
        public string premisesInv { get; set; }         // номер инв.дела ? например: "3893"
        public string premisesUn { get; set; }
        public string literBti { get; set; }            // литер БТИ
        public string premisesType { get; set; }        // код типа объекта, для premises = 002002002000
        public string assignType { get; set; }
        public string premisesName { get; set; }        // название объекта, например "Квартира"
        public decimal areaValue { get; set; }          // площадь объекта
        public string areaType { get; set; }            // код вида площади, 060001003000 = общая (приказ Роснедвижимости от 09.07.07 № П/0160)
        public string areaUnit { get; set; }            // код ЕИ площади по классификатору РР (приказ РР от 13.10.11 № П/389), 012002001000 = 055 (ОКЕИ) = кв.м
        public int premisesFloor { get; set; }          // номер этажа
        public string premisesFloorStr { get; set; }    // номер этажа строкой
        public string premisesNum { get; set; }         // номер квартиры
        public string premisesTypeValue { get; set; }
        public string areaUnitValue { get; set; }
        public bool rightsReg { get; set; }             // признак зарегистрированности прав
        public bool multiFlat { get; set; }             // признак многоквартирности (общага?)
        public string premisesTypeStr { get; set; }     // название типа объекта, например "Помещение"
    }

    /// <summary>
    /// Описывает данные о правах на объект
    /// </summary>
    public class RightEncumbranceObject : IRosreestrData {
        public RightData rightData { get; set; }        // сведения о праве
        public Encumbrance[] encumbrances { get; set; } // сведения об обременениях
    }

    /// <summary>
    /// Описывает данные о праве
    /// </summary>
    public class RightData : IRosreestrData {
        public int tempId { get; set; }
        public string id { get; set; }                  // ИД права
        public string objectId { get; set; }            // ИД объекта
        public int updatePackId { get; set; }
        public int regionKey { get; set; }
        public string code { get; set; }                // код вида права: "001001000000" = "Собственность", "022010000000" = "Доверительное управление"
        public string codeDesc { get; set; }            // название вида права, например "Собственность"
        public string partSize { get; set; }            // размер доли в праве
        public string type { get; set; }
        public string regNum { get; set; }              // регистрационный номер права
        public string regDate { get; set; }             // дата регистрации права
        public string rsCode { get; set; }
        public string packageId { get; set; }
        public string actualDate { get; set; }          // дата актуализации права
    }

    /// <summary>
    /// Описывает данные об обременении
    /// </summary>
    public class Encumbrance : IRosreestrData {
        public int tempId { get; set; }
        public string id { get; set; }                  // ИД обременения
        public string objectId { get; set; }            // ИД объекта
        public int updatePackId { get; set; }
        public int regionKey { get; set; }
        public string code { get; set; }                // код вида права: "001001000000" = "Собственность"
        public string codeDesc { get; set; }            // название вида права, например "Собственность"
        public string periodStart { get; set; }         // дата начала действия
        public string periodEnd { get; set; }           // дата окончания действия
        public string periodDuration { get; set; }      // период действия, например: "с 27.01.2012 по 16.10.2026"
        public object type { get; set; }
        public string regNum { get; set; }              // регистрационный номер обременения
        public string regDate { get; set; }             // дата регистрации
        public string rsCode { get; set; }
        public string packageId { get; set; }
        public string actualDate { get; set; }          // дата актуализации
    }

    /// <summary>
    /// Описывает данные о предыдущем номере объекта
    /// </summary>
    public class OldNumber : IRosreestrData {
        public int tempId { get; set; }
        public string objectId { get; set; }
        public int regionKey { get; set; }
        public string numberType { get; set; }      // код типа номера: "03" = "Кадастровый номер"
        public string numberValue { get; set; }     // номер
        public string normalizedNumberValue { get; set; }
        public string rsCode { get; set; }
        public string packageId { get; set; }
        public string actualDate { get; set; }
        public string numberTypeStr { get; set; }   // тип номера
    }
}
