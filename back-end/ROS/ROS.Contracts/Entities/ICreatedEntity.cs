using System;

namespace ROS.Contracts.Entities
{
	public interface ICreatedEntity
	{
		DateTime? CreatedAt { get; set; }
		string CreatedBy { get; set; }
	}
}
