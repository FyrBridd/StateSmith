using FluentAssertions;
using StateSmith.Runner;
using StateSmith.SmGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace StateSmithTest;

public class TriggerModHelper_Tests
{
    [Fact]
    public void ExampleSimpleLogHelper()
    {
        var inputSmBuilder = new InputSmBuilder();
        inputSmBuilder.transformer.InsertAfterFirstMatch(StandardSmTransformer.TransformationId.Standard_Validation1,
            new TransformationStep(id: "my simple log helper", action: (sm) =>
            {
                Regex regex = new(@"\blog\(\s*\)");

                sm.VisitTypeRecursively<NamedVertex>((namedVertex) =>
                {
                    if (TriggerModHelper.PopModBehaviors(namedVertex, regex).Any())
                    {
                        namedVertex.AddEnterAction($"log(\"Entered {namedVertex.Name}.\");");
                        namedVertex.AddExitAction($"log(\"Exited {namedVertex.Name}.\");");
                    }
                });
        }));

        inputSmBuilder.ConvertPlantUmlTextNodesToVertices(@"
            @startuml SomeSmName
            [*] --> MY_STATE_1
            MY_STATE_1 : $mod / $log();
            MY_STATE_1 : do / stuff();
            @enduml
        ");

        inputSmBuilder.FinishRunning();
        var myState1 = inputSmBuilder.GetStateMachine().Child<State>("MY_STATE_1");

        var behaviorMatcher = VertexTestHelper.BuildFluentAssertionBehaviorMatcher(actionCode: true, guardCode: true, transitionTarget: true, triggers: true);

        myState1.Behaviors.Should().BeEquivalentTo(new List<Behavior>() {
            Behavior.NewEnterBehavior("log(\"Entered MY_STATE_1.\");"),
            Behavior.NewExitBehavior("log(\"Exited MY_STATE_1.\");"),
            new Behavior(trigger:"do", actionCode: "stuff();"),
        }, behaviorMatcher);
    }
}
