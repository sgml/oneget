// 
//  Copyright (c) Microsoft Corporation. All rights reserved. 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  

namespace Microsoft.OneGet.Collections {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ByRefEnumerator : MarshalByRefObject, IEnumerator {
        // we don't want these objects being gc's out because they remain unused...

        private IEnumerator _enumerator;

        public ByRefEnumerator(IEnumerator enumerator) {
            _enumerator = enumerator;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual bool MoveNext() {
            return _enumerator.MoveNext();
        }

        public void Reset() {
            _enumerator.Reset();
        }


        object IEnumerator.Current {
            get {
                // return Current;
                return _enumerator.Current;
            }
        }

        public override object InitializeLifetimeService() {
            return null;
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                var e = _enumerator as IDisposable;
                _enumerator = null;

                if (e != null) {
                    e.Dispose();
                }
                _enumerator = null;
            }
        }
    }
}