class ListUtil:

    @staticmethod
    def equals(list_one, list_two, type_sd=None):
        result_list = []
        map(ListUtil.compare(), list_one, list_two)

    @classmethod
    def compare(x, type_sd):
        pass
