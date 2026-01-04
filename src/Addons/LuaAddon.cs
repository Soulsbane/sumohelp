namespace SumoHelp.Addons;

using Lua;
using Lua.Standard;

class LuaAddon
{
	LuaState _state;
	public LuaState State => _state;

	public LuaValue this [string memberName]
	{
		set => _state.Environment[memberName] = value;
	}

	public LuaAddon() {
		_state = LuaState.Create();
		_state.OpenStandardLibraries();
	}

	public ValueTask<LuaValue[]>CallFunc(string functionName, ReadOnlySpan<LuaValue> args = default)
	{
		if (_state.Environment.ContainsKey(functionName))
		{
			var func = _state.Environment[functionName].Read<LuaFunction>();

			if (args.IsEmpty)
			{
				return _state.CallAsync(func, []);
			}

			return _state.CallAsync(func, args);
		}

		return ValueTask.FromResult(Array.Empty<LuaValue>());
	}

	public void DoString(string luaScript, bool callOnInitialize = true)
	{
		_state.DoStringAsync(luaScript);

		if (callOnInitialize)
		{
			CallFunc("OnInitialize");
		}
	}

	public void DoFile(string filePath)
	{
		_state.DoFileAsync(filePath);
	}
}
