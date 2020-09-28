using System;
using System.Linq;

namespace Kogane.DebugMenu
{
	/// <summary>
	/// シーン遷移の履歴の情報を作成するクラス
	/// </summary>
	[Serializable]
	public sealed class SceneLoadHistoryInfoCreator : ListCreatorBase<ActionData>, IDisposable
	{
		//==============================================================================
		// 変数(static)
		//==============================================================================
		private readonly SceneLoadHistory m_history;

		//==============================================================================
		// 変数
		//==============================================================================
		private ActionData[] m_list;

		//==============================================================================
		// プロパティ
		//==============================================================================
		public override int Count => m_list.Length;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SceneLoadHistoryInfoCreator( int historyCount )
		{
			m_history = new SceneLoadHistory( historyCount );
		}

		/// <summary>
		/// 破棄します
		/// </summary>
		public void Dispose()
		{
			m_history.Dispose();
			m_list = null;
		}

		/// <summary>
		/// リストの表示に使用するデータを作成します
		/// </summary>
		protected override void DoCreate( ListCreateData data )
		{
			m_list = m_history
					.Where( x => data.IsMatch( x.Path, x.LoadSceneMode.ToString() ) )
					.Select( ( val, index ) => new ActionData( $"{( index + 1 ).ToString()},{val.Name},{val.LoadSceneMode.ToString()}" ) )
					.ToArray()
				;

			if ( data.IsReverse )
			{
				Array.Reverse( m_list );
			}
		}

		/// <summary>
		/// 指定されたインデックスの要素の表示に使用するデータを返します
		/// </summary>
		protected override ActionData DoGetElemData( int index )
		{
			return m_list.ElementAtOrDefault( index );
		}
	}
}