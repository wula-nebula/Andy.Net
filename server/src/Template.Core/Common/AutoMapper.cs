using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Common
{
    /// <summary>
    /// 映射
    /// </summary>
    public class AutoMapperExt<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {

        static IMapper Mapper { get; set; }

        static AutoMapperExt()
        {
            var config = new MapperConfiguration(ctx => ctx.CreateMap<TSource, TTarget>());
            Mapper = config.CreateMapper();
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TTarget AutoConvert(TSource entity)
        {
            return Mapper.Map<TSource, TTarget>(entity);
        }

        /// <summary>
        /// 列表类型转换
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<TTarget> MapToList(IEnumerable<TSource> source)
        {
            return Mapper.Map<List<TTarget>>(source);
        }
    }
}
