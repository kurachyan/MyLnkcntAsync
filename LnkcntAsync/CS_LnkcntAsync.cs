using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRSkipAsync;

namespace LnkcntAsync
{
    public class CS_LnkcntAsync
    {
        #region 共有領域
        // '16.01.13 両側余白情報削除の追加　及び、右側・左側余白処理のコメント化
/*
        CS_RskipAsync rskip;        // 右側余白情報を削除
        CS_LskipAsync lskip;        // 左側余白情報を削除
*/
        CS_LRskipAsync lrskip;      // 両側余白情報を削除

        private String _wbuf;       // ソース情報
        private Boolean _empty;     // ソース情報有無
        private int _lnkcnt;        // ネスト情報
        public String Wbuf
        {
            get
            {
                return (_wbuf);
            }
            set
            {
                _wbuf = value;
                if (_wbuf == null)
                {   // 設定情報は無し？
                    _empty = true;
                }
                else
                {   // 整形処理を行う
                    // 不要情報削除
/*
                    if (rskip == null || lskip == null)
                    {   // 未定義？
                        rskip = new CS_RskipAsync();
                        lskip = new CS_LskipAsync();
                    }
                    rskip.Wbuf = _wbuf;
                    rskip.ExecAsync();
                    lskip.Wbuf = rskip.Wbuf;
                    lskip.ExecAsync();
                    _wbuf = lskip.Wbuf;
*/
                    if (lrskip == null)
                    {   // 未定義？
                        lrskip = new CS_LRskipAsync();
                    }
                    lrskip.ExecAsync(_wbuf);
                    _wbuf = lrskip.Wbuf;

                    // 作業の為の下処理
                    if (_wbuf.Length == 0 || _wbuf == null)
                    {   // バッファー情報無し
                        // _wbuf = null;
                        _empty = true;
                    }
                    else
                    {
                        _empty = false;
                    }
                }
            }
        }
        public int Lnkcnt
        {
            get
            {
                return (_lnkcnt);
            }

            set
            {
                _lnkcnt = value;
            }
        }
        #endregion

        #region コンストラクタ
        public CS_LnkcntAsync()
        {   // コンストラクタ
            _wbuf = null;       // 設定情報無し
            _empty = true;
            _lnkcnt = 0;

            lrskip = null;
        }
        #endregion

        #region モジュール
        public async Task ClearAsync()
        {   // 作業領域の初期化
            _wbuf = null;       // 設定情報無し
            _empty = true;
            _lnkcnt = 0;

            lrskip = null;
        }
        public async Task ExecAsync()
        {   // 中カッコ（”｛”、”｝”）のネスト情報を取り出す
            if (!_empty)
            {   // バッファーに実装有り
                int _pos = 0;       // 位置情報
                char[] arry = new char[_wbuf.Length];

                arry = _wbuf.ToCharArray();
                for (_pos = 0; _pos < _wbuf.Length; _pos++)
                {
                    if (arry[_pos] == '{')
                    {   // [｛]有り？
                        _lnkcnt++;
                    }
                    else
                    {
                        if (arry[_pos] == '}')
                        {   // [｝]有り？
                            --_lnkcnt;
                        }
                    }
                }
            }
        }

        public async Task ExecAsync(String msg)
        {   // 中カッコ（”｛”、”｝”）のネスト情報を取り出す
            await SetbufAsync(msg);                 // 入力内容の作業領域設定

            if (!_empty)
            {   // バッファーに実装有り
                int _pos = 0;       // 位置情報
                char[] arry = new char[_wbuf.Length];

                arry = _wbuf.ToCharArray();
                for (_pos = 0; _pos < _wbuf.Length; _pos++)
                {
                    if (arry[_pos] == '{')
                    {   // [｛]有り？
                        _lnkcnt++;
                    }
                    else
                    {
                        if (arry[_pos] == '}')
                        {   // [｝]有り？
                            --_lnkcnt;
                        }
                    }
                }
            }
        }

        private async Task SetbufAsync(String _strbuf)
        {   // [_wbuf]情報設定
            _wbuf = _strbuf;
            if (_wbuf == null)
            {   // 設定情報は無し？
                _empty = true;
            }
            else
            {   // 整形処理を行う
                // 不要情報削除
/*
                if (rskip == null || lskip == null)
                {   // 未定義？
                    rskip = new CS_RskipAsync();
                    lskip = new CS_LskipAsync();
                }
                rskip.Wbuf = _wbuf;
                await rskip.ExecAsync();
                lskip.Wbuf = rskip.Wbuf;
                await lskip.ExecAsync();
                _wbuf = lskip.Wbuf;
*/
                if (lrskip == null)
                {   // 未定義？
                    lrskip = new CS_LRskipAsync();
                }
                await lrskip.ExecAsync(_wbuf);
                _wbuf = lrskip.Wbuf;

                // 作業の為の下処理
                if (_wbuf.Length == 0 || _wbuf == null)
                {   // バッファー情報無し
                    // _wbuf = null;
                    _empty = true;
                }
                else
                {
                    _empty = false;
                }
            }
        }
        #endregion
    }
}
