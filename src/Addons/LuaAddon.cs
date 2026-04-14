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

	public void AddApi(string name, ILuaApi api)
	{
		LuaValue value = LuaValue.FromObject(api.GetInterface());
		_state.Environment[name] = value;
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
		Console.WriteLine($"Executing Lua file: {filePath}");
		try
		{
			_state.DoFileAsync(filePath);
		}
		catch (LuaParseException)
		{
			Console.WriteLine("Error parsing Lua script.");
		}
		catch (LuaRuntimeException)
		{
			Console.WriteLine("Error Running Lua script.");
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}
}
