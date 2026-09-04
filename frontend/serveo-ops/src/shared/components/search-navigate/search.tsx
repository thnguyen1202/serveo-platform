import { SearchIcon } from 'lucide-react';
import { cn } from '@/lib/utils';
import { Button } from '@/shared/components/ui/button';
import { useSearch } from '@/shared/hooks/use-search';
import { useEffect, useState, startTransition } from 'react';
import type { ButtonProps } from 'react-aria-components';

// Định nghĩa kiểu rõ ràng, bóc tách value để tránh lỗi TS2322 với React Aria Button
// 2. Kế thừa trực tiếp từ ButtonProps của React Aria để đồng bộ 100% mọi event (onClick, onFocus, onBlur...)
interface SearchProps extends ButtonProps {
  placeholder?: string;
  value?: string; // Khai báo lại kiểu string chuẩn của React Aria nếu cần dùng bên ngoài
}

export function Search({
  className = '',
  placeholder = 'Search',
  onClick,
  // value, // Bóc tách ra để không bị spread vào <Button>
  ...props
}: SearchProps) {
  const { openSearch } = useSearch();
  const [modifierKey, setModifierKey] = useState('⌘');

  // Tối ưu UI: Tự động đổi ⌘ thành Ctrl nếu người dùng xài Windows/Linux
  useEffect(() => {
    if (typeof navigator !== 'undefined' && !/Mac|iPod|iPhone|iPad/.test(navigator.userAgent)) {
      setModifierKey('Ctrl');
    }
  }, []);

  // Kết hợp onClick từ ngoài truyền vào (nếu có) và logic mở ô search
  const handleSearchClick = (e: any) => {
    onClick?.(e);
    startTransition(() => {
      openSearch();
    });
  };

  return (
    <Button
      {...props}
      variant="outline"
      className={cn(
        'group relative h-8 w-full flex-1 justify-start rounded-md bg-muted/25 text-sm font-normal text-muted-foreground shadow-none hover:bg-accent sm:w-40 sm:pe-12 md:flex-none lg:w-52 xl:w-64',
        className,
      )}
      aria-keyshortcuts="Meta+K Control+K"
      onClick={handleSearchClick}
    >
      {/* Icon kính lúp - Đã fix class inset-s-1.5 chuẩn RTL/LTR logical properties của Tailwind nếu dự án có dùng */}
      <SearchIcon aria-hidden="true" className="absolute inset-s-2 top-1/2 -translate-y-1/2" size={16} />

      <span className="ms-5">{placeholder}</span>

      {/* Kbd hiển thị động theo OS */}
      <kbd className="pointer-events-none absolute inset-e-1.5 top-1/2 -translate-y-1/2 hidden h-5 items-center gap-0.5 rounded border bg-muted px-1.5 font-mono text-[10px] font-medium opacity-100 select-none group-hover:bg-accent sm:flex">
        <span className="text-xs">{modifierKey}</span>K
      </kbd>
    </Button>
  );
}
