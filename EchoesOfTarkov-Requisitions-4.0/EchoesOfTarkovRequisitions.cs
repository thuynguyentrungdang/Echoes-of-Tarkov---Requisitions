using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using Range = SemanticVersioning.Range;

namespace EchoesOfTarkovRequisitions;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.crypluto.echoesoftarkov.requisitions";
    public override string Name { get; init; } = "Echoes Of Tarkov Requisitions";
    public override string Author { get; init; } = "Pluto";
    public override SemanticVersioning.Version Version { get; init; } = new("2.0.3");
    public override Range SptVersion { get; init; } = new("~4.0.0");
    public override string License { get; init; } = "MIT";
    public override bool? IsBundleMod { get; init; } = true;
    public override Dictionary<string, Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~2.0.0") }
    };
    public override string? Url { get; init; }
    public override List<string>? Contributors { get; init; }
    public override List<string>? Incompatibilities { get; init; } = ["ca.bushtail.cantedaiming"];
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
public class EchoesOfTarkovRequisitions(
    ISptLogger<EchoesOfTarkovRequisitions> logger,
    WTTServerCommonLib.WTTServerCommonLib wttCommon,
    DatabaseService databaseService
) : IOnLoad
{
    public required Dictionary<MongoId, TemplateItem> TemplateItems;
    public List<MongoId> MagsToPush65mmRounds_1 = ["5d25a6538abbc306c62e630d",
                                                "5d25a4a98abbc30b917421a4",
                                                "5d25a7b88abbc3054f3e60bc",
                                                "5ce69cbad7f00c00b61c5098",
                                                "5d25a6a48abbc306c62e6310",
                                                "5d25af8f8abbc3055079fec5",
                                                "5cf12a15d7f00c05464b293f",
                                                "5bfeaa0f0db834001b734927",
                                                "5bfea7ad0db834001c38f1ee",
                                                "5df8f535bb49d91fb446d6b0",
                                                "5df8f541c41b2312ea3335e3",
                                                "5a3501acc4a282000d72293a",
                                                "65293c38fc460e50a509cb25",
                                                "65293c7a17e14363030ad308",
                                                "6761770e48fa5c377e06fc3c",
                                                "683997f125039545c12878e9",
                                                "683998905b1e1abba12906fa",
                                                "683998a2a3d4391d033bfd14",
                                                "683998dc60787632112a564b"];

    public List<MongoId> CreedmoreToPush = ["683246a74073e586d112af9f",
                                            "68325a63730b17c69ee176eb",
                                            "6833baea86387d71b68abd6f",
                                            "6833bae4baa31474639bea7d",
                                            "6833ba38a1c9ff155c04e56f",
                                            "6833b8cb54aa764b2deefdfe",
                                            "6834e6f28f5570aec23ca0c2"];

    public List<MongoId> CreedmoreToPush_2 = ["683d1ec4f7164f4f77ea7a85",
                                            "683d1e142cee4317689e75e4",
                                            "683d1ba7b5a2063de35a8c39"];

    public List<MongoId> MagsToPush65mmRounds_2 = ["5d25a6538abbc306c62e630d",
                                                "5d25a4a98abbc30b917421a4",
                                                "5d25a7b88abbc3054f3e60bc",
                                                "5ce69cbad7f00c00b61c5098",
                                                "5d25a6a48abbc306c62e6310",
                                                "5d25af8f8abbc3055079fec5",
                                                "5cf12a15d7f00c05464b293f",
                                                "5bfeaa0f0db834001b734927",
                                                "5bfea7ad0db834001c38f1ee",
                                                "5df8f535bb49d91fb446d6b0",
                                                "5df8f541c41b2312ea3335e3",
                                                "5a3501acc4a282000d72293a",
                                                "65293c38fc460e50a509cb25",
                                                "65293c7a17e14363030ad308",
                                                "6761770e48fa5c377e06fc3c",
                                                "683997f125039545c12878e9",
                                                "683998905b1e1abba12906fa",
                                                "683998a2a3d4391d033bfd14",
                                                "683998dc60787632112a564b"];

    public List<MongoId> SmokeStackSuppressorTo300Blk = ["5fbbfacda56d053a3543f799",
                                                        "5fbbfabed5cb881a7363194e"];

    public List<MongoId> NoveskeKx3ToPush = ["5fbbfacda56d053a3543f799",
                                            "5fbbfabed5cb881a7363194e",
                                            "652910565ae2ae97b80fdf35",
                                            "5dcbe9431e1f4616d354987e",
                                            "6183b084a112697a4b3a6e6c",
                                            "618168b350224f204c1da4d8",
                                            "6183b0711cb55961fa0fdcad",
                                            "5bfebc320db8340019668d79",
                                            "5d2703038abbc3105103d94c"];
    
    public async Task OnLoad()
    {
        TemplateItems = databaseService.GetItems();

        // Get your current assembly
        var assembly = Assembly.GetExecutingAssembly();
        
        // Use WTT-CommonLib services
        await wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly);
        
        AddToFilter();
        
        logger.Success("[Echoes Of Tarkov Requisitions] Mod loaded successfully.");
        
        await Task.CompletedTask;
    }

    public void AddToFilter()
    {
        foreach (MongoId itemId in MagsToPush65mmRounds_1)
        {
            TemplateItemProperties templateItemProperties = TemplateItems[itemId].Properties;

            foreach (MongoId roundId in CreedmoreToPush)
            {
                templateItemProperties.Cartridges.First().Properties.Filters.First().Filter.Add(roundId);
            }
        }
        
        foreach (MongoId itemId in MagsToPush65mmRounds_2)
        {
            TemplateItemProperties templateItemProperties = TemplateItems[itemId].Properties;

            foreach (MongoId roundId in CreedmoreToPush_2)
            {
                templateItemProperties.Cartridges.First().Properties.Filters.First().Filter.Add(roundId);
            }
        }
        
        foreach (MongoId itemId in SmokeStackSuppressorTo300Blk)
        {
            TemplateItemProperties templateItemProperties = TemplateItems[itemId].Properties;
            
            templateItemProperties.Slots.First().Properties.Filters.First().Filter.Add("68343b29f48104d3a6265db3");
            templateItemProperties.Slots.First().Properties.Filters.First().Filter.Add("68343c10334d72dc0066a8f3");
        }
        
        foreach (MongoId itemId in NoveskeKx3ToPush)
        {
            TemplateItemProperties templateItemProperties = TemplateItems[itemId].Properties;
            
            templateItemProperties.Slots.First().Properties.Filters.First().Filter.Add("68395bb9f92a92ea0555e4a1");
            templateItemProperties.Slots.First().Properties.Filters.First().Filter.Add("68395c7abb2e79c208842d7e");
            templateItemProperties.Slots.First().Properties.Filters.First().Filter.Add("56ea6fafd2720b844b8b4593");
        }
        
        List<int> slotsToAdd = [8, 10, 11, 12, 13, 14];

        foreach (int slot in slotsToAdd)
        {
            TemplateItemProperties templateItemProperties = TemplateItems["63dbd45917fff4dee40fe16e"].Properties;
            
            templateItemProperties.Slots.ToArray()[slot].Properties.Filters.First().Filter.Add("68395bb9f92a92ea0555e4a1");
        }
    }

}