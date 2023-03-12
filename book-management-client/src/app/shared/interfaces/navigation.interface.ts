export interface INavigationIem {
  displayName: string;
  disabled?: boolean;
  iconName: string;
  route?: string;
  children?: INavigationIem[];
}
