using CoreMVC.Dependency;

namespace CoreMVC.Service
{
    public interface IEmployeeService : IDependency
    {
        /// <summary>
        /// 统计员工量
        /// </summary>
        /// <returns></returns>
        int CountEmployee();
    }
}
