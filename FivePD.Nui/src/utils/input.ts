import { IDateInput } from '@interfaces';
import { DateFormat } from '@enums';

export const createBirthdateInputList = (dateFormat: DateFormat): IDateInput[] => {
  let inputs: IDateInput[] = [];
  switch (dateFormat as DateFormat) {
    case DateFormat.DMY: {
      inputs = [
        { name: 'Day', placeholder: 'DD' },
        { name: 'Month', placeholder: 'MM' },
        { name: 'Year', placeholder: 'YYYY' }
      ];
      break;
    }
    case DateFormat.MDY: {
      inputs = [
        { name: 'Month', placeholder: 'MM' },
        { name: 'Day', placeholder: 'DD' },
        { name: 'Year', placeholder: 'YYYY' }
      ];
      break;
    }
    case DateFormat.YMD: {
      inputs = [
        { name: 'Year', placeholder: 'YYYY' },
        { name: 'Month', placeholder: 'MM' },
        { name: 'Day', placeholder: 'DD' }
      ];
      break;
    }
  }
  return inputs;
}