import { useState } from 'react';
import { Eye, EyeOff } from 'lucide-react';

import { InputGroup, InputGroupAddon, InputGroupButton, InputGroupInput } from '@/components/ui/input-group';

type PasswordFieldProps = React.ComponentProps<'input'>;

export function Password({ placeholder = '••••••••', ...props }: PasswordFieldProps) {
  const [showPassword, setShowPassword] = useState(false);

  return (
    <InputGroup>
      <InputGroupInput {...props} type={showPassword ? 'text' : 'password'} placeholder={placeholder} />

      <InputGroupAddon align="inline-end">
        <InputGroupButton type="button" variant="ghost" onClick={() => setShowPassword((value) => !value)}>
          {showPassword ? <Eye /> : <EyeOff />}
          <span className="sr-only">{showPassword ? 'Hide password' : 'Show password'}</span>
        </InputGroupButton>
      </InputGroupAddon>
    </InputGroup>
  );
}
