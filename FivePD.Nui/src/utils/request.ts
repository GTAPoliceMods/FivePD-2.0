import { catchError, map, Observable, of, switchMap } from 'rxjs';
import { fromFetch } from 'rxjs/fetch';

export function post<T>(path: string, options: object | number = {}): Observable<T> {
  return request('POST', path, options);
}

function request(method: string, path: string, body: object | number = {}) {

  let newBody: string | number = '';
  if (typeof body === 'object') {
    newBody = JSON.stringify(body);
  } else {
    newBody = body as number;
  }

  return fromFetch(`https://fivepd/${path}`, {
    body: newBody as BodyInit,
    method,
  }).pipe(
    switchMap(res => {
      if (res.ok) {
        return res.json();
      } else {
        return of(new Error(`Error ${res.status}: ${res.statusText}`));
      }
    }),
    catchError(err => {
      return of({
        error: `Error: ${err}`,
      });
    }),
    map(res => {
      if (typeof res === 'string') {
        try {
          return JSON.parse(res);
        } catch(_) {
          return res;
        }
      }

      return res;
    })
  );
}