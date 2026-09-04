import { cn } from '@/lib/utils';

// 1. Đổi HTMLAttributes từ HTMLElement sang HTMLDivElement
type MainProps = React.HTMLAttributes<HTMLDivElement> & {
  as?: React.ElementType; // Cho phép truyền thẻ HTML khác nếu muốn
  fixed?: boolean;
  fluid?: boolean;
  ref?: React.Ref<HTMLDivElement>;
};

export function Content({ as: Component = 'div', fixed, className, fluid, ...props }: MainProps) {
  return (
    // 2. Đổi thẻ <main> thành <div>
    <Component
      data-layout={fixed ? 'fixed' : 'auto'}
      className={cn(
        'px-4 py-6 pt-2',
        fixed && 'flex grow flex-col overflow-hidden',
        !fluid && '@7xl/content:mx-auto @7xl/content:w-full @7xl/content:max-w-7xl',
        className,
      )}
      {...props}
    />
  );
}

// type MainProps = React.HTMLAttributes<HTMLElement> & {
//   fixed?: boolean;
//   fluid?: boolean;
//   ref?: React.Ref<HTMLElement>;
// };

// export function Main({ fixed, className, fluid, ...props }: MainProps) {
//   return (
//     <main
//       data-layout={fixed ? 'fixed' : 'auto'}
//       className={cn(
//         'px-4 py-6',

//         // If layout is fixed, make the main container flex and grow
//         fixed && 'flex grow flex-col overflow-hidden',

//         // If layout is not fluid, set the max-width
//         !fluid && '@7xl/content:mx-auto @7xl/content:w-full @7xl/content:max-w-7xl',
//         className,
//       )}
//       {...props}
//     />
//   );
// }
