using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewYearGift.Comparers.SweetsComparers;
using NewYearGift.Validators;

namespace NewYearGift.Models
{
    /// <summary>
    /// Класс подарка
    /// </summary>
    public class Gift : IGift
    {
        #region fields

        /// <summary>
        /// Словарь хранит элемент позиции подарка и его Id
        /// </summary>
        private readonly IDictionary<int, GiftItem> _giftItems;
        
        /// <summary>
        /// Валидатор позиции подарка
        /// </summary>
        private readonly IValidator<GiftItem> _giftItemValidator;
        #endregion
        
        #region props
        public int Id { get; }
        /// <summary>
        /// Название подарка
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Количество конфет в подарке
        /// </summary>
        public int SweetsCount => _giftItems.Sum(s => s.Value.Count);
        
        /// <summary>
        /// Суммарный вес конфет
        /// </summary>
        public double TotalWeight =>  _giftItems.Sum(s => s.Value.Sweet.Weight * s.Value.Count);
        
        /// <summary>
        /// Суммарная стоимость
        /// </summary>
        public decimal TotalPrice => _giftItems.Sum(s => s.Value.Sweet.Price * s.Value.Count);

        #endregion

        #region ctors

        public Gift(int id, string name)
        {
            _giftItems = new Dictionary<int, GiftItem>();
            _giftItemValidator = new GiftItemValidator();
            
            Name = name;
            Id = id;
        }
        public Gift(int id, string name, IDictionary<int, GiftItem> giftItems):this(id, name)
        {
            _giftItems = giftItems;
        }
        #endregion

        #region methods

        public void Add(GiftItem item)
        {
            _giftItemValidator.Validate(item);

            _giftItems.Add(item.Id, item);
        }
        public GiftItem GetById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id can't be less than 0");
            }

            return _giftItems[id];
        }
        public IReadOnlyList<GiftItem> ListAll()
        {
            return _giftItems.Values.ToList();
        }
        public IReadOnlyList<GiftItem> List(Func<GiftItem, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "Predicate can't be null");
            }

            return _giftItems.Values.Where(predicate).ToList();
        }
        public IReadOnlyList<GiftItem> OrderBy(IComparer<GiftItem> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer), "Comparer can't be null");
            }

            return _giftItems.Values.OrderBy(giftItem => giftItem, comparer).ToList();
        }
        public IReadOnlyList<GiftItem> OrderBy(SweetsOrderRule sweetsOrderRule)
        {
            IComparer<Sweet> comparer = sweetsOrderRule switch
            {
                SweetsOrderRule.Name        => new SweetNameComparer(),
                SweetsOrderRule.Price       => new SweetPriceComparer(),
                SweetsOrderRule.Weight      => new SweetSugarComparer(),
                SweetsOrderRule.SugarWeight => new SweetSugarComparer(),
                _                           => throw new ArgumentOutOfRangeException(
                    nameof(sweetsOrderRule), $"Not expected sweet order rule value: {sweetsOrderRule}"),
            };

            return _giftItems
                .Values
                .OrderBy(giftItem => giftItem.Sweet, comparer)
                .ToList();
        }
        
        public void Update(GiftItem item)
        {
            _giftItemValidator.Validate(item);
            
            int id = item.Id;
            
            if (_giftItems.ContainsKey(id))
            {
                _giftItems[id] = item;
            }
            else
            {
                _giftItems.Add(id, item);
            }
        }
        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id can't be less than 0", nameof(id));
            }

            if (!_giftItems.ContainsKey(id))
            {
                throw new ArgumentException($"Not found gift item with id: {id}", nameof(id));
            }

            _giftItems.Remove(id);
        }

        public IReadOnlyList<Sweet> GetSweetsBySugarRange(double min, double max)
        {
            if (min < 0)
            {
                throw new ArgumentException("Min value can't be less than 0", nameof(min));
            }

            if (max < 0)
            {
                throw new ArgumentException("Max value can't be less than 0", nameof(max));
            }

            if (min > max)
            {
                throw new ArgumentException("Max value can't be less than min value", nameof(max));
            }

            return _giftItems.Values
                .Select(giftItem => giftItem.Sweet)
                .Where(sweet => sweet.SugarWeight >= min && sweet.SugarWeight <= max)
                .ToList();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Стоимость подарка: {TotalPrice:C2}");
            sb.AppendLine($"Вес подарка: {TotalWeight} г.");
            sb.AppendLine($"Список конфет:");
            
            foreach (var giftItem in _giftItems)
            {
                sb.AppendLine($"{giftItem.Key}. {giftItem.Value.Sweet}, Количество: {giftItem.Value.Count}");
            }

            return sb.ToString();
        }

        #endregion
    }
}
