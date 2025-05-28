import type { BaseNuiEvent } from '@interfaces';
import type { NuiEventType } from '@enums';
import { Subject, filter, Observable } from 'rxjs';

const eventHandler = new Subject<BaseNuiEvent>();

export function nuiEventNext(data: BaseNuiEvent) {
  eventHandler.next(data);
}

export function onNuiEvent<T>(...events: NuiEventType[]): Observable<T> {
  return eventHandler
    .pipe(
      filter((event: BaseNuiEvent) => (events as string[]).includes(event.type))
    ) as any as Observable<T>;
}
