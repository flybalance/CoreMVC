using CoreMVC.Dependency;
using CoreMVC.Domain.Entity;
using System.Collections.Generic;


namespace CoreMVC.Service
{
    public interface IMemberService: IDependency
    {
        /// <summary>
        /// 获取单个会员 信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Member GetMemberInfo(long id);

        /// <summary>
        /// 获取会员集合
        /// </summary>
        /// <returns></returns>
        List<Member> ListMember();

        /// <summary>
        /// 统计会员量
        /// </summary>
        /// <returns></returns>
        int CountMember();
    }
}
