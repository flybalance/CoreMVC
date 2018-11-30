using CoreMVC.Domain.Entity;
using System.Collections.Generic;

namespace CoreMVC.Service.Impl
{
    public class MemberService : IMemberService
    {
        public Member GetMemberInfo(long id)
        {
            return new Member() { Id = id, Name = "test", Company = "锤子科技" };
        }

        public List<Member> ListMember()
        {
            return new List<Member>();
        }

        public int CountMember()
        {
            return int.MaxValue;
        }
    }
}
