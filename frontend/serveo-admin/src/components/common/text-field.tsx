import { useFormContext } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Input } from '../ui/input';

// type Props = {
//   name: string;
//   label: string;
//   register: UseFormRegister<any>;
//   error?: FieldError;
//   placeholder?: string;
// };

export function TextField({ name, label, placeholder }: { name: string; label?: string; placeholder?: string }) {
  const { t } = useTranslation(['common']);
  const {
    register,
    formState: { errors },
  } = useFormContext();
  const error = errors[name]?.message as string;
  placeholder ??= label;

  return (
    <div className="space-y-2">
      {label && (
        <label htmlFor={name} className="text-sm font-medium">
          {label}
        </label>
      )}

      <Input id={name} placeholder={placeholder} {...register(name)} aria-invalid={!!error} data-invalid={!!error} />

      {/* {error && <p className="text-sm text-destructive">{error}</p>} */}
      {error && <p className="text-sm text-destructive">{t(error, { ns: 'common' })}</p>}
    </div>
  );
}

// export function TextField({ name, label, register, error, placeholder }: Props) {
//   return (
//     <div className="space-y-2">
//       <label htmlFor={name} className="text-sm font-medium">
//         {label}
//       </label>

//       <Input id={name} placeholder={placeholder} {...register(name)} />

//       {error && <p className="text-sm text-destructive">{error.message}</p>}
//     </div>
//   );
// }
