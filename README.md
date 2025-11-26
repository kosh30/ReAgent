# ReAgent

## Fork Information

This is a fork of [exApiTools/ReAgent](https://github.com/exApiTools/ReAgent) with many party-related features and enhancements added.

My Discord: pepeg11

### Major Additions

#### Party Features
- `PartyMembersInZone` - Track party members in your current zone
- `IsInParty` - Check if you're in a party
- `PartyMembers` - Get list of all party members
- `NearbyPartyMembers(int distance)` - Find party members within a specific range
- `AnyPartyMemberMissingBuff(string buffName, int withinDistance)` - Check if any party member is missing a buff
- `AnyPartyMemberHasBuff(string buffName, int withinDistance)` - Check if any party member has a buff
- `PartyMemberByName(string name, int maxDistance)` - Find specific party member by name
- `PartyMemberHereAndNoGracePeriod(string name, int maxDistance)` - Check if party member is nearby and not in grace period
- `PartyMemberByClass(string className, int maxDistance)` - Find party member by class
- `PartyClassHereAndNoGracePeriod(string className, int maxDistance)` - Check if a class is nearby and not in grace period

#### Vaal Skill Coordination
- `ShouldCastVaalSkillByPriority(string skillName, string buffName, float radius, float castBeforeExpiry)` - Smart Vaal skill casting with priority system (Scion > Duelist > Others)

#### Zone Transition Tracking
- `JustLeftLoadingScreen` - Detect when loading screen ends
- `JustEnteredArea` - Detect when entering a new area
- `TimeSinceAreaEnter` - Time elapsed since entering current area
- `TimeSinceLastTransition` - Time elapsed since last zone transition
- `JustFinishedTransition` - Detect when zone transition completes
- `IsInGracePeriod` - Check grace period status
- `IgnoreGracePeriod` - Group-level setting to ignore grace period checks

#### Other Enhancements
- `PlayerScreenPosition` - Get player position on screen
- `SinceLastActivation(double minTime, double maxTime)` - Improved timing with randomization
- Portal tracking improvements
- Exerted attacks support
- UI crash fixes (credit: rushtothesun)

### Testing Status

**Note:** Not all features are thoroughly tested.
- ✅ **Party-related features** - Tested and working well (for me at least :) )
- ⚠️ **Transition tracking** - Mostly untested
- ⚠️ **Vaal skill coordination** - Untested

If you encounter any issues or bugs, feel free to reach out on Discord!

## Support

If you like this project, you can donate original author of ReAgent via:

BTC: bc1qke67907s6d5k3cm7lx7m020chyjp9e8ysfwtuz

ETH: 0x3A37B3f57453555C2ceabb1a2A4f55E0eB969105

