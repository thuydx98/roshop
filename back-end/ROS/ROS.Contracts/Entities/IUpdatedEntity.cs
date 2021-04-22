using System;

namespace ROS.Contracts.Entities
{
	public interface IUpdatedEntity
	{
		DateTime? UpdatedAt { get; set; }
		string UpdatedBy { get; set; }
	}
}
