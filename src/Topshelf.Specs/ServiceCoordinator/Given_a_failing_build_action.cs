﻿// Copyright 2007-2010 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Topshelf.Specs.ServiceCoordinator
{
	using System;
	using Magnum.TestFramework;
	using NUnit.Framework;
	using TestObject;


    [Scenario, Explicit("Need new scenarios for faults")]
	[Slow]
	public class Given_a_failing_build_action :
		ServiceCoordinator_SpecsBase
	{
		[When]
		public void A_registered_service_cannot_be_built()
		{
			CreateService<TestService>("test",
			                           x => x.Start(),
			                           x => x.Stop(),
			                           x => x.Pause(),
			                           x => x.Continue(),
			                           (x, c) => { throw new Exception(); });
		}

		[Then]
		public void An_exception_is_throw_when_service_is_paused()
		{
			Assert.That(() => Coordinator.Start(), Throws.InstanceOf<Exception>());
		}
	}
}